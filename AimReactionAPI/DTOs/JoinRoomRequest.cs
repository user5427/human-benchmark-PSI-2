using WebSocketBoilerplate;
namespace AimReactionAPI.DTOs;
public class JoinRoomRequest(int playerId, Guid roomId) : BaseDto
{
    public int PlayerId { get; set; } = playerId;
    public required Guid RoomId { get; set; } = roomId;
}