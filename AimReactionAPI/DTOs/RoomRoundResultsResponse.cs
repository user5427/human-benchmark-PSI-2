
using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class RoomRoundResultsResponse(List<RoomPlayerDto> remainingPlayers, List<RoomPlayerDto> eliminatedPlayers) : BaseDto
{
    public List<RoomPlayerDto> RemainingPlayers { get; init; } = remainingPlayers ?? [];
    public List<RoomPlayerDto> EliminatedPlayers { get; init; } = eliminatedPlayers ?? [];
}