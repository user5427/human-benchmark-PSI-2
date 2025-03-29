using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserService _userService;
    public UsersController(AppDbContext context, UserService userService)
    {
        _context = context;
        _userService = userService;
    }


    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetUsers([FromQuery] int userId)
    {
        return await _userService.GetUsers(userId);
    }
}

