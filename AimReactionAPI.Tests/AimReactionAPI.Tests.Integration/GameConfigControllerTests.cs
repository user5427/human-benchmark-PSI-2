using System.Runtime.CompilerServices;
using NUnit;
using Moq;
using AimReactionAPI.Data;
using AimReactionAPI.Services;
using AimReactionAPI.Controllers;
using AimReactionAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AimReactionAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Validations;

namespace AimReactionAPI.Tests.Integration
{
    [TestFixture]
    public class GameConfigControllerTests
    {
        private AppDbContext _context;
        private GameService _gameService;
        private GameUserService _gameUserService;
        private Mock<ILogger<GameService>> _gameServiceLoggerMock;
        private TargetService _targetService;
        private ILogger<GameConfigController> _logger;
        private GameConfigController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _context.Users.Add(new User
            {
                UserId = 1,
                Name = "test1",
                Email = "test1@gmail.com",
                PasswordHash = "hashed"
            });
            _context.Users.Add(new User
            {
                UserId = 2,
                Name = "test2",
                Email = "test2@gmail.com",
                PasswordHash = "hashed"
            });
            _context.SaveChanges();
            _gameServiceLoggerMock = new Mock<ILogger<GameService>>();
            _targetService = new TargetService(_context);
            _gameUserService = new GameUserService(_context);
            _gameService = new GameService(_context, _gameServiceLoggerMock.Object, _targetService, _gameUserService);
            _controller = new GameConfigController(_context, _logger, _gameService);
        }

        [Test]
        public async Task CreateOrUpdateGame_ValidDto_ReturnsOkRequest()
        {
            var gameConfigDto = new GameConfigDto
            {
                Name = "Test Game",
                Description = "Description",
                DifficultyLevel = "Difficulty",
                TargetSpeed = 1,
                MaxTargets = 1,
                GameDuration = 1,
                GameType = GameType.MovingTargets,
                AllowedUsers = []
            };

            var result = await _controller.CreateOrUpdateGame(gameConfigDto);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var createdGame = okResult.Value as Game;
            Assert.IsNotNull(createdGame);
            Assert.AreEqual("Test Game", createdGame.GameName);
        }

        [Test]
        public async Task CreateOrUpdateGame_gameConfigDtoIsNull_ReturnsBadRequest()
        {
            var result = await _controller.CreateOrUpdateGame(null);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Invalid game configuration data.", badRequestResult.Value);
        }

        [Test]
        public async Task CreateOrUpdateGame_gameIsNull_ReturnsServerError()
        {
            var gameConfigDto = new GameConfigDto
            {
                Name = "Test Game",
                Description = "Description",
                DifficultyLevel = "Difficulty",
                TargetSpeed = 1,
                MaxTargets = 1,
                GameDuration = 1,
                GameType = GameType.MovingTargets,
                AllowedUsers = []
            };

            _gameService = new GameService(_context, _gameServiceLoggerMock.Object, _targetService, _gameUserService);
            _controller = new GameConfigController(_context, _logger, new GameServiceStub(null));

            var result = await _controller.CreateOrUpdateGame(gameConfigDto);

            Assert.IsInstanceOf<ObjectResult>(result);
            var serverErrorResult = result as ObjectResult;
            Assert.AreEqual(500, serverErrorResult.StatusCode);
            Assert.AreEqual("Operation failed.", serverErrorResult.Value);
        }

        [Test]
        public async Task CreateOrUpdateGame_ExistingGamePublic_UpdatesAndReturnsOk()
        {
            var existingGame = new Game
            {
                GameId = 1,
                CreatorId = 1,
                GameName = "Existing Game",
                GameDescription = "Old Description",
                DifficultyLevel = "Easy",
                TargetSpeed = 1,
                MaxTargets = 5,
                GameDuration = 10,
                GameType = GameType.ReflexTest,
                Visibility = GameVisibility.PUBLIC
            };

            _context.Games.Add(existingGame);
            await _context.SaveChangesAsync();

            var gameConfigDto = new GameConfigDto
            {
                GameId = 1,
                CreatorId = 1,
                Name = "Updated Game",
                Description = "New Description",
                DifficultyLevel = "Hard",
                TargetSpeed = 2,
                MaxTargets = 10,
                GameDuration = 20,
                GameType = GameType.MovingTargets,
                Visibility = GameVisibility.PUBLIC,
                AllowedUsers = []
            };

            var result = await _controller.CreateOrUpdateGame(gameConfigDto);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var updatedGame = okResult.Value as Game;
            Assert.IsNotNull(updatedGame);
            Assert.AreEqual("Updated Game", updatedGame.GameName);
            Assert.AreEqual("New Description", updatedGame.GameDescription);
            Assert.AreEqual("Hard", updatedGame.DifficultyLevel);
            Assert.AreEqual(2, updatedGame.TargetSpeed);
            Assert.AreEqual(10, updatedGame.MaxTargets);
            Assert.AreEqual(20, updatedGame.GameDuration);
            Assert.AreEqual(GameType.MovingTargets, updatedGame.GameType);
        }

        [Test]
        public async Task CreateOrUpdateGame_ExistingGamePrivate_UpdatesAndReturnsOk()
        {
            var existingGame = new Game
            {
                GameId = 1,
                CreatorId = 1,
                GameName = "Existing Game",
                GameDescription = "Old Description",
                DifficultyLevel = "Easy",
                TargetSpeed = 1,
                MaxTargets = 5,
                GameDuration = 10,
                GameType = GameType.ReflexTest,
                Visibility = GameVisibility.PUBLIC
            };

            _context.Games.Add(existingGame);
            await _context.SaveChangesAsync();

            var gameConfigDto = new GameConfigDto
            {
                GameId = 1,
                CreatorId = 1,
                Name = "Updated Game",
                Description = "New Description",
                DifficultyLevel = "Hard",
                TargetSpeed = 2,
                MaxTargets = 10,
                GameDuration = 20,
                GameType = GameType.MovingTargets,
                Visibility = GameVisibility.PRIVATE,
                AllowedUsers = [2]
            };

            var result = await _controller.CreateOrUpdateGame(gameConfigDto);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var updatedGame = okResult.Value as Game;
            Assert.IsNotNull(updatedGame);
            Assert.AreEqual("Updated Game", updatedGame.GameName);
            Assert.AreEqual("New Description", updatedGame.GameDescription);
            Assert.AreEqual("Hard", updatedGame.DifficultyLevel);
            Assert.AreEqual(2, updatedGame.TargetSpeed);
            Assert.AreEqual(10, updatedGame.MaxTargets);
            Assert.AreEqual(20, updatedGame.GameDuration);
            Assert.AreEqual(GameType.MovingTargets, updatedGame.GameType);
        }

        [Test]
        public async Task CreateOrUpdateGame_UserNotCreator_ReturnsForbidden()
        {
            var existingGame = new Game
            {
                GameId = 2,
                CreatorId = 456,
                GameName = "Existing Game",
                GameDescription = "New Description",
                DifficultyLevel = "Hard",
            };

            _context.Games.Add(existingGame);
            await _context.SaveChangesAsync();

            var gameConfigDto = new GameConfigDto
            {
                GameId = 2,
                CreatorId = 123,
                Name = "Unauthorized Update",
                Description = "New Description",
                DifficultyLevel = "Hard",
                AllowedUsers = []
            };

            var result = await _controller.CreateOrUpdateGame(gameConfigDto);

            Assert.IsInstanceOf<ObjectResult>(result);
            var badRequest = result as ObjectResult;
            Assert.AreEqual(400, badRequest.StatusCode);
            Assert.AreEqual("User is not allowed to make changes.", badRequest.Value);
        }

        [TearDown]
        //cleanup database
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

    public class GameServiceStub : GameService
    {
        private readonly Game _returnValue;

        public GameServiceStub(Game returnValue) : base(null)
        {
            _returnValue = returnValue;
        }

        public override Task<Game?> CreateOrUpdateGameAsync(GameConfigDto gameConfig)
        {
            return Task.FromResult(_returnValue);
        }
    }
}