using AimReactionAPI.Models;
using WebSocketBoilerplate;
namespace AimReactionAPI.DTOs;

public class CreateRoomRequest(int playerId, string roomName,
    GameVisibility visibility, HashSet<int> allowedPlayers) : BaseDto
{
    public int PlayerId { get; set; } = playerId;
    public required string RoomName { get; set; } = roomName;
    public GameVisibility Visibility { get; set; } = visibility;
    public HashSet<int> AllowedPlayers { get; set; } = allowedPlayers;
}
