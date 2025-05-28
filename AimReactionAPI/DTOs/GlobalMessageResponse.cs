namespace AimReactionAPI.DTOs;

public class GlobalMessageResponse(string content, string sender, DateTime createdAt) : MessageResponse(content, sender, createdAt)
{
}
