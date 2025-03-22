using System.Collections;

namespace AimReactionAPI.Models;

public class GameUser
{
    public int GameId { get; set; }
    public int UserId { get; set; }
    public virtual Game? Game { get; set; }
    public virtual User? User { get; set; }
}
