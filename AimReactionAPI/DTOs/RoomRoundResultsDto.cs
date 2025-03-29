
namespace AimReactionAPI.DTOs;

public record RoomRoundResultsDto(List<RoomPlayerDto> RemainingPlayers, List<RoomPlayerDto> EliminatedPlayers);