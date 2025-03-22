using AimReactionAPI.Data;
using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace AimReactionAPI.Services;

public class GameUserService
{
    private readonly AppDbContext _context;
    private readonly ILogger<GameService> _logger;
    private readonly object value;

    //added stubs in testing
    public GameUserService(object value)
    {
        this.value = value;
    }

    public GameUserService(AppDbContext context, ILogger<GameService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SetGameUsersAsync(int gameId, List<int> userIds)
    {
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
        await _context.GameUsers
            .Where(gu => gu.GameId == gameId)
            .ExecuteDeleteAsync();
    }
}
