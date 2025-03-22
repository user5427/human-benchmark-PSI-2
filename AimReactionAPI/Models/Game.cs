using System.Collections;

namespace AimReactionAPI.Models;

public class Game : IEnumerable<Target>
{
    public int GameId { get; set; }
    public string GameName { get; set; }
    public string GameDescription { get; set; }
    public string DifficultyLevel { get; set; }
    public int TargetSpeed { get; set; }
    public int MaxTargets { get; set; }
    public int GameDuration { get; set; }
    public int CreatorId { get; set; }
    public GameVisibility Visibility { get; set; }
    public GameType GameType { get; set; }
    public ICollection<Target> Targets { get; set; }
    public ICollection<Score> Scores { get; set; }
    public virtual User Creator { get; set; }
    public virtual ICollection<GameUser> GameUsers { get; set; }
    public Game()
    {
        Targets = new List<Target>();
    }

    public IEnumerator<Target> GetEnumerator()
    {
        return Targets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
