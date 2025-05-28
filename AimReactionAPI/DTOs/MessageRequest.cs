using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class MessageRequest(int sender, string content) : BaseDto
{
    public int SenderId { get; set; } = sender;
    public string Content { get; set; } = content;
}
