using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;

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
        try
        {
            Game game;

            if (gameConfig.GameId.HasValue)
            {
                game = await _context.Games.FindAsync(gameConfig.GameId.Value)
                    ?? throw new Exception("Game not found");

                if (game.CreatorId != gameConfig.CreatorId)
                    throw new UnauthorizedAccessException("User is not allowed to make changes.");
            }
            else
            {
                game = new Game
                {
                    CreatorId = gameConfig.CreatorId,
                };
                _context.Games.Add(game);
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

            await _context.SaveChangesAsync();

            if (game.Visibility == GameVisibility.PRIVATE)
            {
                await _gameUserService.SetGameUsersAsync(game.GameId, gameConfig.AllowedUsers);
            }
            else if (gameConfig.GameId.HasValue)
            {
                await _gameUserService.DeleteCurrentGameUsersAsync(game.GameId);
            }

            return game;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating or updating a game.");
            return null;
        }
    }
}
