using AimReactionAPI.Models;

namespace AimReactionAPI.DTOs;

public class GameDto
{
    public int GameId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string DifficultyLevel { get; set; }
    public int TargetSpeed { get; set; }
    public int MaxTargets { get; set; }
    public int CreatorId { get; set; }
    public int GameDuration { get; set; }
    public GameType GameType { get; set; }
    public GameVisibility Visibility { get; set; }
    public required List<int> AllowedUsers { get; set; }

}
