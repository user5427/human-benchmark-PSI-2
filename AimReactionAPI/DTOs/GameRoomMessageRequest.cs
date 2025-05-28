namespace AimReactionAPI.DTOs;

public class GameRoomMessageRequest(Guid gameRoomId, int senderId, string content) : MessageRequest(senderId, content)
{
    public Guid GameRoomId { get; set; } = gameRoomId;
}
