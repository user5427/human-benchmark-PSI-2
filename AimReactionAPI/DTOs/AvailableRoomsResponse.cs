using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class AvailableRoomsResponse(List<RoomResponse> rooms) : BaseDto
{
    public List<RoomResponse> Rooms { get; set; } = rooms;
}
