using WebSocketBoilerplate;
namespace AimReactionAPI.DTOs;

public class CreateRoomRequest(int playerId, string roomName) : BaseDto
{
    public int PlayerId { get; set; } = playerId;
    public required string RoomName { get; set; } = roomName;
}
