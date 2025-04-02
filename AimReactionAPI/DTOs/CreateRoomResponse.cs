using WebSocketBoilerplate;
namespace AimReactionAPI.DTOs;

public class CreateRoomResponse(Guid roomId) : BaseDto
{
    public Guid RoomId { get; set; } = roomId;
}
