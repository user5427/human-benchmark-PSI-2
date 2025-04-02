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
    private readonly ILogger<MultiplayerService> _logger;
    private const int ROUND_DURATION_SECONDS = 5;
    public MultiplayerService(IServiceProvider serviceProvider, ILogger<MultiplayerService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task Connect(int playerId, IWebSocketConnection ws)
    {
        _logger.LogInformation($"player({playerId}) is connecting");
        using var scope = _serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<UserService>();
        User? user = await userService.FindUser(playerId) ??
            throw new InvalidDataException($"User with ID {playerId} not found.");

        Player player = new(user.Name, ws);
        Players.TryAdd(playerId, player);
        SendMessageToPlayer(playerId, JsonSerializer.Serialize(new AvailableRoomsResponse(GetJoinableRooms())));
        _logger.LogInformation($"player({playerId}) connected");
    }

    public void CreateRoom(int playerId, string roomName)
    {
        _logger.LogInformation($"player({playerId}) is creating room({roomName})");
        if (!Players.ContainsKey(playerId))
        {
            throw new InvalidDataException($"User {playerId} not found.");
        }
        ValidateCanJoin(playerId);
        Guid roomGuid = Guid.NewGuid();
        Room room = new(roomGuid, playerId, roomName);
        Rooms.TryAdd(roomGuid, room);
        SendMessageToPlayer(playerId, JsonSerializer.Serialize(GetRoomResponse(room)));
        BroadcastToAll(JsonSerializer.Serialize(new AvailableRoomsResponse(GetJoinableRooms())));
        _logger.LogInformation($"player({playerId}) created room({roomName})");
    }

    public void JoinRoom(int playerId, Guid roomId)
    {
        _logger.LogInformation($"player({playerId}) is joining room({roomId})");
        if (!Players.ContainsKey(playerId) ||
            !Rooms.TryGetValue(roomId, out var room))
        {
            throw new InvalidDataException($"User {playerId} Or room {roomId} not found");
        }
        ValidateCanJoin(playerId, roomId);
        room.AddToRoom(playerId);
        BroadcastMessageToRoom(room, JsonSerializer.Serialize(GetRoomResponse(room)));
        _logger.LogInformation($"player({playerId}) joined room({roomId})");
    }
    public void StartRoom(int playerId, Guid roomId)
    {
        _logger.LogInformation($"player({playerId}) is starting room({roomId})");
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
        _logger.LogInformation($"player({playerId}) started room({roomId})");
    }

    public void RegisterTargetHit(int playerId, Guid roomId, double reactionTime)
    {
        _logger.LogInformation($"registering hit for player({playerId}) in room({roomId})");
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
        _logger.LogInformation($"registered hit for player({playerId}) in room({roomId})");

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
        _logger.LogInformation($"player({playerId}) is disconnecting");
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
        _logger.LogInformation($"player({playerId}) disconnected");
    }

    private void ValidateCanJoin(int playerId, Guid roomId = new())
    {
        foreach (var (id, room) in Rooms)
        {
            if (room.Players.Contains(playerId) && id != roomId)
            {
                throw new InvalidOperationException($"User {playerId} is already in the room {id}");
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
        _logger.LogInformation($"round of ({room.Id}) ended");

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

    private void BroadcastToAll(string message)
    {
        foreach (var player in Players)
        {
            SendMessageToPlayer(player.Key, message);
        }
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
        _logger.LogInformation($"round of ({room.Id}) started");
        room.ResetPlayerTimes();
        Target target = TargetService.GenerateTarget();
        CreateAndBroadcastTargetToRoom(room, target);
        Task.Delay(ROUND_DURATION_SECONDS * 1000).ContinueWith(t => HandleRoundEnd(room));
    }
}