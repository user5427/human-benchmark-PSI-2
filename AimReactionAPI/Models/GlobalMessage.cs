namespace AimReactionAPI.Models;

public class GlobalMessage
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual User? Sender { get; set; }
}
