using AimReactionAPI.Models;

namespace AimReactionAPI.DTOs;

public class MiniGameDto
{
    public int GameId { get; set; }
    public int CreatorId { get; set; }
    public GameDescription GameDescription { get; set; }
    public string GameDifficulty { get; set; }
}