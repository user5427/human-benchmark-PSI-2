using System.Text.Json;
using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Services;

public class MessageService
{
    private readonly RoomChatStateService _roomChatStateService;
    private readonly AppDbContext _dbContext;
    private readonly MultiplayerService _multiplayerService;
    private readonly UserService _userService;
    private readonly ILogger<MessageService> _logger;

    public MessageService(
        AppDbContext dbContext,
        MultiplayerService multiplayerService,
        UserService userService,
        RoomChatStateService roomChatStateService,
        ILogger<MessageService> logger)
    {
        _dbContext = dbContext;
        _roomChatStateService = roomChatStateService;
        _multiplayerService = multiplayerService;
        _userService = userService;
        _logger = logger;
    }

    public async Task SendGlobalMessage(GlobalMessageRequest request)
    {
        _logger.LogInformation("Sending global message from user ID {UserId}", request.SenderId);

        User user = await _userService.FindUser(request.SenderId)
            ?? throw new UnauthorizedAccessException();

        GlobalMessage message = new GlobalMessage
        {
            Content = request.Content,
            SenderId = user.UserId,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.GlobalMessages.Add(message);
        await _dbContext.SaveChangesAsync();

        GlobalMessageResponse response = new(message.Content, user.Name, message.CreatedAt);

        _multiplayerService.Broadcast(JsonSerializer.Serialize(response));
    }

    public async Task<List<GlobalMessageResponse>> GetGlobalMessages(int userId)
    {
        _logger.LogInformation("Fetching global messages for user ID {UserId}", userId);

        var user = await _userService.FindUser(userId)
            ?? throw new UnauthorizedAccessException();

        return await _dbContext.GlobalMessages
            .OrderBy(m => m.CreatedAt)
            .Select(m => new GlobalMessageResponse(m.Content, m.Sender.Name, m.CreatedAt))
            .ToListAsync();
    }

    public async Task SendGameRoomMessage(GameRoomMessageRequest request)
    {
        _logger.LogInformation("Sending game room message from user ID {UserId} to room {RoomId}", request.SenderId, request.GameRoomId);

        var user = await _userService.FindUser(request.SenderId);
        if (user == null
            || !_multiplayerService.TryGetRoom(request.GameRoomId, out Room? room)
            || (room != null && !room.Players.Contains(request.SenderId)))
        {
            throw new UnauthorizedAccessException();
        }

        GameRoomMessageResponse message = new(
            request.GameRoomId,
            request.Content,
            user.Name,
            DateTime.UtcNow
        );

        _roomChatStateService.SaveMessage(message);
        _multiplayerService.BroadcastToRoom(room!, JsonSerializer.Serialize(message));
    }

    public async Task<List<GameRoomMessageResponse>> GetGameRoomMessages(int userId, Guid gameRoomId)
    {
        _logger.LogInformation("Fetching game room messages for user ID {UserId} in room {RoomId}", userId, gameRoomId);

        if (await _userService.FindUser(userId) == null
            || !_multiplayerService.TryGetRoom(gameRoomId, out Room? _))
        {
            throw new UnauthorizedAccessException();
        }

        return _roomChatStateService.GetMessages(gameRoomId);
    }
}
