using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;
public class HitTargetRequest(int playerId, Guid roomId, double reactionTime) : BaseDto
{
    public int PlayerId { get; set; } = playerId;
    public required Guid RoomId { get; set; } = roomId;
    public double ReactionTime { get; set; } = reactionTime;
}