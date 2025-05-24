using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class MessageResponse(string content, string sender, DateTime createdAt) : BaseDto
{
    public string Content { get; set; } = content;
    public string Sender { get; set; } = sender;
    public DateTime CreatedAt { get; set; } = createdAt;
}
