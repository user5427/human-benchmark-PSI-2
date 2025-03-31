using System.Collections.Concurrent;
using System.Text.Json;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using Fleck;

namespace AimReactionAPI.Services;

public class MultiplayerService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<int, Player> Players = new();
    private readonly ConcurrentDictionary<Guid, Room> Rooms = new();
    private const int ROUND_DURATION_SECONDS = 5;
    public MultiplayerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Connect(int playerId, IWebSocketConnection ws)
    {
        using var scope = _serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<UserService>();
        User? user = await userService.FindUser(playerId) ??
            throw new InvalidDataException($"User with ID {playerId} not found.");

        Player player = new(user.Name, ws);
        Players.TryAdd(playerId, player);
    }

    public void CreateRoom(int playerId, string roomName)
    {
        if (!Players.ContainsKey(playerId))
        {
            throw new InvalidDataException($"User {playerId} not found.");
        }
        ValidateNotPlaying(playerId);
        Guid roomGuid = Guid.NewGuid();
        Room room = new(roomGuid, playerId, roomName);
        Rooms.TryAdd(roomGuid, room);
        BroadcastMessageToRoom(room, JsonSerializer.Serialize(GetRoomResponse(room)));
    }

    public void JoinRoom(int playerId, Guid roomId)
    {
        if (!Players.ContainsKey(playerId) ||
            !Rooms.TryGetValue(roomId, out var room))
        {
            throw new InvalidDataException($"User {playerId} Or room {roomId} not found");
        }
        ValidateNotPlaying(playerId);
        room.AddToRoom(playerId);
        BroadcastMessageToRoom(room, JsonSerializer.Serialize(GetRoomResponse(room)));
    }
    public void StartRoom(int playerId, Guid roomId)
    {
        if (!Rooms.TryGetValue(roomId, out var room))
        {
            throw new InvalidDataException($"Room {roomId} not found");
        }
        if (room.CreatorId != playerId)
        {
            throw new InvalidOperationException($"User {playerId} not allowed to start.");
        }
        if (room.Players.Count < 2)
        {
            throw new InvalidOperationException($"Minimum 2 players required(Room {roomId}).");
        }
        room.RoomStatus = RoomStatus.PLAYING;
        StartRound(room);
    }

    public void RegisterTargetHit(int playerId, Guid roomId, double reactionTime)
    {
        if (!Rooms.TryGetValue(roomId, out var room) ||
            !room.Players.Contains(playerId))
        {
            throw new InvalidDataException($"User {playerId} not in room Or room {roomId} not found");
        }
        if (room.RoomStatus != RoomStatus.PLAYING)
        {
            throw new InvalidOperationException($"Room {roomId} is not in a playing state.");
        }
        room.RegisterPlayerHit(playerId, reactionTime);
    }

    public List<RoomResponse> GetJoinableRooms()
    {
        return Rooms.Values
        .Where(room => room.RoomStatus == RoomStatus.WAITING)
        .Select(room => new RoomResponse(
            room.Id,
            room.Name,
            room.CreatorId,
            room.Players
                .Select(id => Players[id].Username)
                .ToList(),
            room.RoomStatus.ToString()))
        .ToList();
    }

    public void Disconnect(int playerId)
    {
        if (!Players.TryRemove(playerId, out var player))
        {
            return;
        }
        foreach (var room in Rooms)
        {
            room.Value.RemoveFromRoom(playerId);
            if (room.Value.Players.Count == 0)
            {
                Rooms.TryRemove(room);
            }
        }

        player.Connection.Close();
    }

    private void ValidateNotPlaying(int playerId)
    {
        foreach (var (roomId, room) in Rooms)
        {
            if (room.Players.Contains(playerId))
            {
                throw new InvalidOperationException($"User {playerId} is already in the room {roomId}");
            }
        }
    }

    private RoomResponse GetRoomResponse(Room room)
    {
        return new RoomResponse(
            room.Id,
            room.Name,
            room.CreatorId,
            room.Players
                .Select(id => Players[id].Username)
                .ToList(),
            room.RoomStatus.ToString());
    }

    private void BroadcastRoundResults(Room room, HashSet<int> eliminatedPlayerIds)
    {
        List<RoomPlayerDto> eliminatedPlayers = Players.Where(p => eliminatedPlayerIds.Contains(p.Key))
                    .Select(p => new RoomPlayerDto(p.Value.Username, p.Key, room.PlayerTimes.GetValueOrDefault(p.Key)))
                    .ToList();
        List<RoomPlayerDto> remainingPlayers = Players.Where(p => room.Players.Contains(p.Key) && !eliminatedPlayerIds.Contains(p.Key))
                    .Select(p => new RoomPlayerDto(p.Value.Username, p.Key, room.PlayerTimes.GetValueOrDefault(p.Key)))
                    .ToList();
        var results = new RoomRoundResultsResponse(remainingPlayers, eliminatedPlayers);
        var serializedResults = JsonSerializer.Serialize(results);
        BroadcastMessageToRoom(room, serializedResults);
    }

    private void SendMessageToPlayer(int playerId, string message)
    {
        if (Players.TryGetValue(playerId, out var player))
        {
            player.Connection.Send(message);
        }
    }

    private void HandleRoundEnd(Room room)
    {
        var eliminatedPlayers = room.Players
            .Where(playerId => !room.PlayerTimes.ContainsKey(playerId))
            .ToHashSet();
        if (eliminatedPlayers.Count == 0)
        {
            var slowestPlayer = room.PlayerTimes
                        .OrderBy(p => p.Value)
                        .FirstOrDefault();
            eliminatedPlayers.Add(slowestPlayer.Key);
        }

        BroadcastRoundResults(room, eliminatedPlayers);
        Thread.Sleep(1000);
        foreach (var player in eliminatedPlayers)
        {
            room.RemoveFromRoom(player);
        }
        if (room.Players.Count > 1)
        {
            StartRound(room);
        }
        else
        {
            Rooms.TryRemove(room.Id, out var _);
        }
    }

    private void CreateAndBroadcastTargetToRoom(Room room, Target target)
    {
        var targetDto = new TargetResponse(target.X, target.Y);
        var serializedTarget = JsonSerializer.Serialize(targetDto);
        BroadcastMessageToRoom(room, serializedTarget);
    }

    private void BroadcastMessageToRoom(Room room, string message)
    {
        foreach (var playerId in room.Players)
        {
            SendMessageToPlayer(playerId, message);
        }
    }
    
    private void StartRound(Room room)
    {
        room.ResetPlayerTimes();
        Target target = TargetService.GenerateTarget();
        CreateAndBroadcastTargetToRoom(room, target);
        Task.Delay(ROUND_DURATION_SECONDS * 1000).ContinueWith(t => HandleRoundEnd(room));
    }
}