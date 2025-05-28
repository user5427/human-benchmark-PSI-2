using System.Text.Json.Serialization;
using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class MessageResponse(string content, string sender, DateTime createdAt) : BaseDto
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = content;
    [JsonPropertyName("sender")]
    public string Sender { get; set; } = sender;
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = createdAt;
}
