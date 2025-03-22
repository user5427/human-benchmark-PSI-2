using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenericGameController : ControllerBase
{
    private readonly GameSessionHandler<GameType> _gameSessionHandler;
    private readonly AppDbContext _context;

    public GenericGameController(GameSessionHandler<GameType> gameSessionHandler, AppDbContext context)
    {
        _gameSessionHandler = gameSessionHandler;
        _context = context;
    }

    [HttpPost("{userId}/start/{gameType}")]
    public async Task<IActionResult> StartGameSession(int userId, GameType gameType)
    {
        var session = await _gameSessionHandler.StartSessionAsync(userId, gameType);
        return Ok(session);
    }

    [HttpPost("end/{sessionId}")]
    public async Task<IActionResult> EndGameSession(int sessionId)
    {
        var duration = await _gameSessionHandler.EndSessionAsync(sessionId);
        return Ok(duration);
    }

    [HttpGet("games")]
    public async Task<ActionResult> GetAllGames([FromQuery] int userId)
    {
        var games = await _context.Games
            .Where(g =>
                g.CreatorId == userId ||
                g.Visibility == GameVisibility.PUBLIC ||
                _context.GameUsers.Any(gu => gu.GameId == g.GameId && gu.UserId == userId)
            )
            .Select(g => new
            {
                g.GameId,
                g.CreatorId,
                GameDescription = new GameDescription(g.GameName, g.GameDescription, g.GameType),
                GameDifficulty = g.DifficultyLevel
            })
            .ToListAsync();

        return Ok(games);
    }

    [HttpGet("games/{id}")]
    public async Task<ActionResult<GameDto>> GetGameById(int id, [FromQuery] int userId)
    {
        Game? game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == id);

        if (game == null)
        {
            return NotFound("Game not found");
        }

        if (!HasAccess(game, userId))
        {
            return Unauthorized("Unauthorized access");
        }

        List<int> allowedUsers = _context.GameUsers
                                    .Where(gu => gu.GameId == game.GameId)
                                    .Select(gu => gu.UserId)
                                    .ToList();

        var gameDto = new GameDto
        {
            GameId = game.GameId,
            Name = game.GameName,
            Description = game.GameDescription,
            DifficultyLevel = game.DifficultyLevel,
            TargetSpeed = game.TargetSpeed,
            MaxTargets = game.MaxTargets,
            GameDuration = game.GameDuration,
            GameType = game.GameType,
            AllowedUsers = allowedUsers,
            CreatorId = game.CreatorId,
            Visibility = game.Visibility
        };

        return Ok(gameDto);
    }

    [HttpGet("games/{id}/targets")]
    public async Task<ActionResult<IEnumerable<Target>>> GetGameTargets(int id)
    {
        Game? game = await _context.Games
            .Include(g => g.Targets)
            .FirstOrDefaultAsync(g => g.GameId == id);

        if (game == null)
        {
            return NotFound("Game not found");
        }

        return Ok(game.Targets);
    }

    [HttpDelete("games/{id}")]
    public async Task<IActionResult> DeleteGame(int id, [FromQuery] int userId)
    {
        Game? game = await _context.Games.FindAsync(id);

        if (game == null)
        {
            return NotFound("Game not found");
        }

        if (!HasAccess(game, userId))
        {
            return Unauthorized("Unauthorized access");
        }

        _context.Games.Remove(game);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{userId}/addscore")]
    public async Task<IActionResult> AddScore([FromRoute] int userId, [FromBody] AddScoreDto scoreDto)
    {
        Console.WriteLine($"Received gameId: {scoreDto.GameId}, userId: {userId}"); ;
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == scoreDto.GameId);
        if (game == null)
        {
            return NotFound("Game not found");
        }

        _context.Attach(game);

        var newScore = new Score
        {
            Value = scoreDto.Value,
            DateAchieved = scoreDto.DateAchieved,
            GameId = scoreDto.GameId,
            GameType = scoreDto.GameType,
            UserId = userId
        };

        _context.Scores.Add(newScore);
        await _context.SaveChangesAsync();

        return Ok(newScore);
    }

    [HttpGet("active")]
    public IActionResult GetActiveSessionCount()
    {
        var activeCount = _gameSessionHandler.GetActiveSessionCount();
        return Ok(new { activeSessions = activeCount });
    }


    private bool HasAccess(Game game, int userId)
    {
        return game.CreatorId == userId ||
            game.Visibility == GameVisibility.PUBLIC ||
            _context.GameUsers.Any(gu => gu.GameId == game.GameId && gu.UserId == userId);
    }
}
