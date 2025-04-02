using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Services;

public class GameService
{
    private readonly AppDbContext _context;
    private readonly ILogger<GameService> _logger;
    private readonly TargetService _targetService;
    private readonly GameUserService _gameUserService;
    private object value;

    //added stubs in testing
    public GameService(object value)
    {
        this.value = value;
    }

    public GameService(AppDbContext context, ILogger<GameService> logger, TargetService targetService, GameUserService gameUserService)
    {
        _context = context;
        _logger = logger;
        _targetService = targetService;
        _gameUserService = gameUserService;
    }
    public virtual async Task<Game?> CreateOrUpdateGameAsync(GameConfigDto gameConfig)
    {
        if (gameConfig == null)
        {
            throw new ArgumentNullException("Game configuration cannot be null.");
        }

        try
        {
            Game game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameConfig.GameId)
                        ?? new Game { CreatorId = gameConfig.CreatorId };

            if (gameConfig.GameId.HasValue && game.CreatorId != gameConfig.CreatorId)
            {
                throw new UnauthorizedAccessException("User is not allowed to make changes.");
            }

            game.GameName = gameConfig.Name;
            game.GameDescription = gameConfig.Description;
            game.DifficultyLevel = gameConfig.DifficultyLevel;
            game.TargetSpeed = gameConfig.TargetSpeed;
            game.MaxTargets = gameConfig.MaxTargets;
            game.GameDuration = gameConfig.GameDuration;
            game.Visibility = gameConfig.Visibility;
            game.GameType = gameConfig.GameType;
            game.Targets = _targetService.GenerateTargets(gameConfig.MaxTargets, gameConfig.TargetSpeed);

            if (!gameConfig.GameId.HasValue)
            {
                _context.Games.Add(game);
            }

            await _context.SaveChangesAsync();

            if (game.Visibility == GameVisibility.PRIVATE)
            {
                await _gameUserService.SetGameUsersAsync(game.GameId, gameConfig.AllowedUsers ?? new List<int>());
            }
            else if (gameConfig.GameId.HasValue)
            {
                await _gameUserService.DeleteCurrentGameUsersAsync(game.GameId);
            }

            return game;
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized users attempts to create/update a game.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating or updating a game.");
            return null;
        }
    }
}
