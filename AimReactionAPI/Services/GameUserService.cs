using AimReactionAPI.Data;
using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace AimReactionAPI.Services;

public class GameUserService
{
    private readonly AppDbContext _context;
    private readonly object value;

    //added stubs in testing
    public GameUserService(object value)
    {
        this.value = value;
    }

    public GameUserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task SetGameUsersAsync(int gameId, List<int> userIds)
    {
        if (userIds == null || userIds.Count == 0)
        {
            return;
        }

        await DeleteCurrentGameUsersAsync(gameId);

        var gameUsers = userIds.Select(userId => new GameUser
        {
            GameId = gameId,
            UserId = userId
        }).ToList();

        await _context.GameUsers.AddRangeAsync(gameUsers);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCurrentGameUsersAsync(int gameId)
    {
        var usersToDelete = await _context.GameUsers.Where(gu => gu.GameId == gameId).ToListAsync();
        if (usersToDelete.Count != 0)
        {
            _context.GameUsers.RemoveRange(usersToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
