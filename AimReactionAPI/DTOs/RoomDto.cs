
using AimReactionAPI.Models;

namespace AimReactionAPI.DTOs;

public record RoomDto(Guid Id, string Name, int CreatorId, int PlayerCount, string RoomStatus);
