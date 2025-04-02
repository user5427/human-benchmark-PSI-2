using WebSocketBoilerplate;
namespace AimReactionAPI.DTOs;
public class StartRoomRequest(int playerId, Guid roomId) : BaseDto
{
    public int PlayerId { get; set; } = playerId;
    public Guid RoomId { get; set; } = roomId;
}
