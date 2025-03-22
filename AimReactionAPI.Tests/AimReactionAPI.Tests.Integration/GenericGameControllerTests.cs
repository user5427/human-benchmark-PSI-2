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
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace AimReactionAPI.Tests.Integration
{
    [TestFixture]
    public class GenericGameControllerTests
    {
        private AppDbContext _context;
        private GenericGameController _controller;
        private GameSessionHandler<GameType> _gameSessionHandler;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;
            _context = new AppDbContext(options);

            SeedDatabase();

            var mockScopeFactory = new Mock<IServiceScopeFactory>();
            var mockScope = new Mock<IServiceScope>();
            var mockServiceProvider = new Mock<IServiceProvider>();

            mockScopeFactory.Setup(x => x.CreateScope()).Returns(mockScope.Object);
            mockScope.Setup(x => x.ServiceProvider).Returns(mockServiceProvider.Object);
            mockServiceProvider.Setup(x => x.GetService(typeof(AppDbContext))).Returns(_context);

            _gameSessionHandler = new GameSessionHandler<GameType>(_context, mockScopeFactory.Object);
            _controller = new GenericGameController(_gameSessionHandler, _context);
        }



        [Test]
        public async Task StartGameSession_ShouldReturnSession()
        {
            var result = await _controller.StartGameSession(1, GameType.ReflexTest);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var gameSessionResult = okResult.Value as GameSession;
            Assert.IsNotNull(gameSessionResult);

            Assert.AreEqual(1, gameSessionResult.UserId);
            Assert.AreEqual(GameType.ReflexTest, gameSessionResult.GameType);
        }

        [Test]
        public async Task GetAllGames_ShouldReturnOnlyPublic()
        {
            int userId = 3;
            var expectedGameCount = 1;

            var result = await _controller.GetAllGames(userId);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var games = okResult.Value as List<MiniGameDto>;
            Assert.AreEqual(expectedGameCount, games.Count);
        }

        [Test]
        public async Task GetAllGames_ShouldReturnPublicAndPrivate_AccessGiven()
        {
            int userId = 2;
            var expectedGameCount = 2;

            var result = await _controller.GetAllGames(userId);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var games = okResult.Value as List<MiniGameDto>;
            Assert.AreEqual(expectedGameCount, games.Count);
        }

        [Test]
        public async Task GetAllGames_ShouldReturnPublicAndPrivate_Creator()
        {
            int userId = 1;
            var expectedGameCount = 2;

            var result = await _controller.GetAllGames(userId);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var games = okResult.Value as List<MiniGameDto>;
            Assert.AreEqual(expectedGameCount, games.Count);
        }


        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private async Task SeedDatabase()
        {
            var game1 = new Game
            {
                GameId = 1,
                GameName = "Test1",
                GameDescription = "Description.",
                DifficultyLevel = "Medium",
                TargetSpeed = 50,
                MaxTargets = 10,
                GameDuration = 30,
                CreatorId = 1,
                Visibility = GameVisibility.PUBLIC,
                GameType = GameType.ReflexTest
            };

            var game2 = new Game
            {
                GameId = 2,
                GameName = "Test2",
                GameDescription = "Description.",
                DifficultyLevel = "Medium",
                TargetSpeed = 50,
                MaxTargets = 10,
                GameDuration = 30,
                CreatorId = 1,
                Visibility = GameVisibility.PRIVATE,
                GameType = GameType.ReflexTest
            };

            var user1 = new User { UserId = 1, Name = "test", Email = "test@example.com", PasswordHash = "hash" };
            var user2 = new User { UserId = 2, Name = "test2", Email = "test2@example.com", PasswordHash = "hash" };
            var user3 = new User { UserId = 3, Name = "test3", Email = "test3@example.com", PasswordHash = "hash" };

            _context.Users.AddRange(user1, user2, user3);
            _context.Games.AddRange(game1, game2);

            _context.GameUsers.Add(new GameUser { UserId = 2, GameId = 2 });

            await _context.SaveChangesAsync();
        }
    }
}
