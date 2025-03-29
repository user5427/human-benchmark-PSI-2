using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Rooms : ControllerBase
{
    private readonly MultiplayerService _multiplayerService;
    private readonly UserService _userService;
    public Rooms(UserService userService, MultiplayerService multiplayerService)
    {
        _userService = userService;
        _multiplayerService = multiplayerService;
    }

    [HttpGet]
    public List<RoomDto> GetRooms([FromQuery] int userId)
    {
        return _multiplayerService.GetJoinableRooms();
    }
}

