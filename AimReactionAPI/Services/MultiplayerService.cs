using System.Collections.Concurrent;
using System.Text.Json;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using Fleck;
using Microsoft.IdentityModel.Tokens;

namespace AimReactionAPI.Services;

public class MultiplayerService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<int, Player> _players = new();
    private readonly ConcurrentDictionary<Guid, Room> _rooms = new();
    private readonly ILogger<MultiplayerService> _logger;
    private const int ROUND_DURATION_SECONDS = 5;
    private const int UI_UPDATE_DURATION_SECONDS = 5;
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
        _players.TryAdd(playerId, player);
        SendMessageToPlayer(playerId, JsonSerializer.Serialize(new AvailableRoomsResponse(GetJoinableRooms(playerId))));
        _logger.LogInformation($"player({playerId}) connected");
    }

    public void CreateRoom(int playerId, string roomName, GameVisibility visibility, HashSet<int> playersWithAccess)
    {
        _logger.LogInformation($"player({playerId}) is creating room({roomName})");
        if (!_players.ContainsKey(playerId))
        {
            throw new InvalidDataException($"User {playerId} not found.");
        }
        ValidateCanJoin(playerId);  
        Guid roomGuid = Guid.NewGuid();
        Room room = new(roomGuid, playerId, roomName, visibility, playersWithAccess);
        _rooms.TryAdd(roomGuid, room);
        SendMessageToPlayer(playerId, JsonSerializer.Serialize(GetRoomResponse(room)));
        BroadcastJoinableGames();
        _logger.LogInformation($"player({playerId}) created room({roomName})");
    }

    public void JoinRoom(int playerId, Guid roomId)
    {
        _logger.LogInformation($"player({playerId}) is joining room({roomId})");
        if (!_players.ContainsKey(playerId) ||
            !_rooms.TryGetValue(roomId, out var room))
        {
            throw new InvalidDataException($"User {playerId} Or room {roomId} not found");
        }
        ValidateCanJoin(playerId, roomId);
        room.AddToRoom(playerId);
        BroadcastToRoom(room, JsonSerializer.Serialize(GetRoomResponse(room)));
        _logger.LogInformation($"player({playerId}) joined room({roomId})");
    }
    public void StartRoom(int playerId, Guid roomId)
    {
        _logger.LogInformation($"player({playerId}) is starting room({roomId})");
        if (!_rooms.TryGetValue(roomId, out var room))
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
        if (!_rooms.TryGetValue(roomId, out var room) ||
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

    public List<RoomResponse> GetJoinableRooms(int playerId)
    {
        return _rooms.Values
        .Where(room => room.RoomStatus == RoomStatus.WAITING &&
                (room.RoomVisibility == GameVisibility.PUBLIC ||
                (room.RoomVisibility == GameVisibility.PRIVATE &&
                room.AllowedPlayers.Contains(playerId)))
            )
        .Select(room => new RoomResponse(
            room.Id,
            room.Name,
            room.CreatorId,
            room.Players
                .Select(id => _players[id].Username)
                .ToList(),
            room.RoomStatus.ToString()))
        .ToList();
    }

    public void Disconnect(int playerId)
    {
        _logger.LogInformation($"player({playerId}) is disconnecting");
        if (!_players.TryRemove(playerId, out var player))
        {
            return;
        }
        foreach (var room in _rooms)
        {
            room.Value.RemoveFromRoom(playerId);
            if (room.Value.Players.Count == 0)
            {
                _rooms.TryRemove(room);
            }
        }
        player.Connection.Close();
        _logger.LogInformation($"player({playerId}) disconnected");
    }

    public bool TryGetRoom(Guid roomId, out Room? room)
    {
        return _rooms.TryGetValue(roomId, out room);
    }

    public void Broadcast(string message)
    {
        foreach (var (playerId, _) in _players)
        {
            SendMessageToPlayer(playerId, message);
        }
    }
    public void BroadcastToRoom(Room room, string message)
    {
        foreach (var playerId in room.Players)
        {
            SendMessageToPlayer(playerId, message);
        }
    }

    private void ValidateCanJoin(int playerId, Guid roomId = new())
    {
        foreach (var (id, room) in _rooms)
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
                .Select(id => _players[id].Username)
                .ToList(),
            room.RoomStatus.ToString());
    }

    private void BroadcastRoundResults(Room room, HashSet<int> eliminatedPlayerIds)
    {
        List<RoomPlayerDto> eliminatedPlayers = _players.Where(p => eliminatedPlayerIds.Contains(p.Key))
                    .Select(p => new RoomPlayerDto(p.Value.Username, p.Key, room.PlayerTimes.GetValueOrDefault(p.Key)))
                    .ToList();
        List<RoomPlayerDto> remainingPlayers = _players.Where(p => room.Players.Contains(p.Key) && !eliminatedPlayerIds.Contains(p.Key))
                    .Select(p => new RoomPlayerDto(p.Value.Username, p.Key, room.PlayerTimes.GetValueOrDefault(p.Key)))
                    .ToList();
        var results = new RoomRoundResultsResponse(remainingPlayers, eliminatedPlayers);
        var serializedResults = JsonSerializer.Serialize(results);
        BroadcastToRoom(room, serializedResults);
    }

    private void SendMessageToPlayer(int playerId, string message)
    {
        if (_players.TryGetValue(playerId, out var player))
        {
            player.Connection.Send(message);
        }
    }

    private async void HandleRoundEnd(Room room)
    {
        _logger.LogInformation($"round of room({room.Id}) ended");

        var eliminatedPlayers = room.Players
            .Where(playerId => !room.PlayerTimes.ContainsKey(playerId))
            .ToHashSet();
        if (eliminatedPlayers.Count == 0)
        {
            var slowestPlayer = room.PlayerTimes
                        .OrderByDescending(p => p.Value)
                        .FirstOrDefault();
            eliminatedPlayers.Add(slowestPlayer.Key);
        }
        _logger.LogInformation($"eliminated player ids: {string.Join(", ", eliminatedPlayers)}");

        BroadcastRoundResults(room, eliminatedPlayers);
        await Task.Delay(UI_UPDATE_DURATION_SECONDS * 1000); 
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
            _rooms.TryRemove(room.Id, out var _);
        }
    }

    private void CreateAndBroadcastTargetToRoom(Room room, Target target)
    {
        var targetDto = new TargetResponse(target.X, target.Y);
        var serializedTarget = JsonSerializer.Serialize(targetDto);
        BroadcastToRoom(room, serializedTarget);
    }

    private void BroadcastJoinableGames()
    {
        foreach (var (playerId, _) in _players)
        {
            var joinableRooms = GetJoinableRooms(playerId);
            var response = new AvailableRoomsResponse(joinableRooms);
            if (!joinableRooms.IsNullOrEmpty())
                SendMessageToPlayer(playerId, JsonSerializer.Serialize(response));
        }
    }
  
    private void StartRound(Room room)
    {
        _logger.LogInformation($"round of room({room.Id}) started");
        room.ResetPlayerTimes();
        Target target = TargetService.GenerateTarget();
        CreateAndBroadcastTargetToRoom(room, target);
        Task.Delay(ROUND_DURATION_SECONDS * 1000).ContinueWith(t => HandleRoundEnd(room));
    }
}