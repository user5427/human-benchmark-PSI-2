namespace AimReactionAPI.DTOs;

public class GlobalMessageRequest(int senderId, string content) : MessageRequest(senderId, content)
{
}
