using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDto>> GetUsers(int userId)
    {
        return await _context.Users
                        .Where(u => u.UserId != userId)
                        .Select(u => new UserDto(u.Name, u.UserId))
                        .ToListAsync();
    }

    public virtual async Task<User?> FindUser(int userId)
    {
        return await _context.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefaultAsync();
    }
}
