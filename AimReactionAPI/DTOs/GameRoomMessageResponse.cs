namespace AimReactionAPI.DTOs;

public class GameRoomMessageResponse(Guid gameRoomId, string content, string sender, DateTime createdAt) : MessageResponse(content, sender, createdAt)
{
    public Guid GameRoomId { get; set; } = gameRoomId;
}
