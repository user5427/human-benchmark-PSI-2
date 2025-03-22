using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetUsers([FromQuery] int userId)
    {
        return await _context.Users
                        .Where(u => u.UserId != userId)
                        .Select(u => new UserDto(u.Name, u.UserId))
                        .ToListAsync();
    }
}

