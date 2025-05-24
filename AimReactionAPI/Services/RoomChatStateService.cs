using System.Collections.Concurrent;
using AimReactionAPI.DTOs;

namespace AimReactionAPI.Services;

public class RoomChatStateService
{
    private readonly ConcurrentDictionary<Guid, List<GameRoomMessageResponse>> _roomMessages = new();

    public void SaveMessage(GameRoomMessageResponse message)
    {
        _roomMessages.AddOrUpdate(
            message.GameRoomId,
            _ => new List<GameRoomMessageResponse> { message },
            (_, list) => { list.Add(message); return list; });
    }

    public List<GameRoomMessageResponse> GetMessages(Guid gameRoomId)
    {
        return _roomMessages.TryGetValue(gameRoomId, out var messages)
            ? messages
            : new();
    }

    public void DeleteRoomMessages(Guid gameRoomId)
    {
         _roomMessages.TryRemove(gameRoomId, out _);
    }
}