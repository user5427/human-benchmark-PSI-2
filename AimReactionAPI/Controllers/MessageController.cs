using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly MessageService _messageService;
    public MessageController(MessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet("global")]
    public async Task<ActionResult<List<GlobalMessageResponse>>> GetGlobalMessages([FromQuery(Name = "user-id")] int userId)
    {
        return await _messageService.GetGlobalMessages(userId);
    }
    
    [HttpGet("room")]
    public async Task<ActionResult<List<GameRoomMessageResponse>>> GetGameRoomMessages([FromQuery(Name = "user-id")] int userId, [FromQuery(Name = "room-id")] Guid roomId)
    {
        return await _messageService.GetGameRoomMessages(userId, roomId);
    }
}

