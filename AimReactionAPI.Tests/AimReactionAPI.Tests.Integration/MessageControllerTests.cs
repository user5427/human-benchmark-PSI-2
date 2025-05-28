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
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.Extensions.DependencyInjection;

namespace AimReactionAPI.Tests.Integration;

[TestFixture]
public class MessageControllerTests
{
    private AppDbContext _context;
    private MessageController _controller;
    private ILogger<MessageController> _logger;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services.AddLogging();
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("testDb"));

        services.AddScoped<UserService>();
        services.AddScoped<MultiplayerService>();
        services.AddSingleton<RoomChatStateService>();
        services.AddScoped<MessageService>();
        services.AddScoped<MessageController>();

        var serviceProvider = services.BuildServiceProvider();

        _context = serviceProvider.GetRequiredService<AppDbContext>();
        _logger = serviceProvider.GetRequiredService<ILogger<MessageController>>();
        _controller = serviceProvider.GetRequiredService<MessageController>();

        SeedDatabase();
    }

    [Test]
    public async Task GetGlobalMessages_ShouldReturnMessasages_WhenValidUser()
    {
        var result = await _controller.GetGlobalMessages(1);

        Assert.IsInstanceOf<ActionResult<List<GlobalMessageResponse>>>(result);

        var messages = result.Value;
        Assert.IsNotNull(messages);
        Assert.AreEqual(2, messages.Count);

        Assert.AreEqual("test message 1", messages[0].Content);
        Assert.AreEqual("test message 2", messages[1].Content);
        Assert.AreEqual("test", messages[0].Sender);
        Assert.AreEqual("test1", messages[1].Sender);
    }

    [Test]
    public async Task GetGlobalMessages_ShouldReturnError_WhenInvalidUser()
    {
        var result = await _controller.GetGlobalMessages(99);

        Assert.IsInstanceOf<ActionResult<List<GlobalMessageResponse>>>(result);

        var unauthorizedResult = result.Result as ObjectResult;

        Assert.IsNotNull(unauthorizedResult);
        Assert.AreEqual(401, unauthorizedResult.StatusCode);
        Assert.AreEqual("User is not authorized", unauthorizedResult.Value);
    }


    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    public void SeedDatabase()
    {

        _context.Users.AddRange(
                new User {UserId = 1, Name = "test", Email = "test@example.com", PasswordHash = "hash"},
                new User { UserId = 2, Name = "test1", Email = "test1@example.com", PasswordHash = "hash1" }
            );
        _context.GlobalMessages.AddRange(
                new GlobalMessage { Id = 1, SenderId = 1, Content = "test message 1", CreatedAt = DateTime.Now},
                new GlobalMessage { Id = 2, SenderId = 2, Content = "test message 2", CreatedAt = DateTime.Now}
            );  
        _context.SaveChangesAsync();
    }
}
