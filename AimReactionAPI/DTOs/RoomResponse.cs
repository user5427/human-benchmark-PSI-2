using AimReactionAPI.Models;
using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class RoomResponse(Guid id, string name, int creatorId, List<string> players, string roomStatus) : BaseDto
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public int CreatorId { get; set; } = creatorId;
    public List<string> Players { get; set; } = players;
    public string RoomStatus { get; set; } = roomStatus;
}
