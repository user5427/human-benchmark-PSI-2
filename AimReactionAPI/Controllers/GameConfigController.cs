using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameConfigController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<GameConfigController> _logger;
    private readonly GameService _gameService;

    public GameConfigController(AppDbContext context, ILogger<GameConfigController> logger, GameService gameService)
    {
        _context = context;
        _logger = logger;
        _gameService = gameService;
    }


    // PUT api/gameconfig
    [HttpPut]
    public async Task<IActionResult> CreateOrUpdateGame([FromBody] GameConfigDto gameConfig)
    {
        if (gameConfig == null)
        {
            return BadRequest("Invalid game configuration data.");
        }
        try
        {
            Game? game = await _gameService.CreateOrUpdateGameAsync(gameConfig);

            if (game == null)
            {
                return StatusCode(500, "Operation failed.");
            }

            return Ok();
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(400, ex.Message);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while saving game configuration.");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
