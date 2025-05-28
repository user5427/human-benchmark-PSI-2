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
        try
        {
            return await _messageService.GetGlobalMessages(userId);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("User is not authorized");
        }
        catch (Exception)
        {
            return StatusCode(500, $"Unexpected error occurred");
        }
    }

    [HttpGet("room")]
    public async Task<ActionResult<List<GameRoomMessageResponse>>> GetGameRoomMessages(
        [FromQuery(Name = "user-id")] int userId, 
        [FromQuery(Name = "room-id")] Guid roomId)
    {
        try
        {
            return await _messageService.GetGameRoomMessages(userId, roomId);
        }
         catch (UnauthorizedAccessException)
        {
            return Unauthorized("User is not authorized");
        }
        catch (Exception)
        {
            return StatusCode(500, $"Unexpected error occurred");
        }
    }
}

