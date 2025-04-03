using NUnit.Framework;
using Moq;
using AimReactionAPI.Services;
using AimReactionAPI.Models;
using AimReactionAPI.DTOs;
using AimReactionAPI.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Fleck;
using System.Text.Json;
using System.Collections.Concurrent; 
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Tests.Unit
{
    [TestFixture]
    public class MultiplayerServiceTests
    {
        private Mock<IServiceProvider> _mockServiceProvider;
        private Mock<ILogger<MultiplayerService>> _mockLogger;
        private Mock<UserService> _mockUserService; 
        private Mock<AppDbContext> _mockDbContext;
        private MultiplayerService _multiplayerService;

        private Dictionary<int, Mock<IWebSocketConnection>> _mockConnections;

        private const int Player1Id = 1;
        private const string Player1Name = "PlayerOne";
        private const int Player2Id = 2;
        private const string Player2Name = "PlayerTwo";
        private const int Player3Id = 3;
        private const string Player3Name = "PlayerThree";

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<MultiplayerService>>();
            _mockServiceProvider = new Mock<IServiceProvider>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
            _mockDbContext = new Mock<AppDbContext>(options); 

            _mockUserService = new Mock<UserService>(_mockDbContext.Object);

            var mockScope = new Mock<IServiceScope>();
            var mockScopeFactory = new Mock<IServiceScopeFactory>();

            mockScope.Setup(s => s.ServiceProvider).Returns(_mockServiceProvider.Object);
            mockScopeFactory.Setup(sf => sf.CreateScope()).Returns(mockScope.Object);
            _mockServiceProvider.Setup(sp => sp.GetService(typeof(IServiceScopeFactory))).Returns(mockScopeFactory.Object);

            _mockServiceProvider
                .Setup(sp => sp.GetService(typeof(UserService))) 
                .Returns(_mockUserService.Object); 

            _multiplayerService = new MultiplayerService(_mockServiceProvider.Object, _mockLogger.Object);
            _mockConnections = new Dictionary<int, Mock<IWebSocketConnection>>();

            _mockUserService.Setup(us => us.FindUser(Player1Id)).ReturnsAsync(new User { UserId = Player1Id, Name = Player1Name });
            _mockUserService.Setup(us => us.FindUser(Player2Id)).ReturnsAsync(new User { UserId = Player2Id, Name = Player2Name });
            _mockUserService.Setup(us => us.FindUser(Player3Id)).ReturnsAsync(new User { UserId = Player3Id, Name = Player3Name });
            _mockUserService.Setup(us => us.FindUser(It.IsNotIn(Player1Id, Player2Id, Player3Id)))
                            .ReturnsAsync((User?)null);
        }

        private Mock<IWebSocketConnection> SetupMockConnection(int playerId)
        {
            var mockConnection = new Mock<IWebSocketConnection>();
            mockConnection.Setup(ws => ws.Send(It.IsAny<string>())).Verifiable(); 
            mockConnection.Setup(ws => ws.Close()).Verifiable(); 
            _mockConnections[playerId] = mockConnection;
            return mockConnection;
        }

        private async Task ConnectPlayer(int playerId)
        {
            var mockConn = SetupMockConnection(playerId);
            await _multiplayerService.Connect(playerId, mockConn.Object);
            mockConn.Verify(ws => ws.Send(It.Is<string>(s => s.Contains("\"Rooms\":[]"))), Times.Once); 
            mockConn.Invocations.Clear();
        }


        [Test]
        public async Task Connect_WhenUserExists_AddsPlayerAndSendsAvailableRooms()
        {
            // Arrange
            var mockConn = SetupMockConnection(Player1Id);

            // Act
            await _multiplayerService.Connect(Player1Id, mockConn.Object);

            // Assert
            mockConn.Verify(ws => ws.Send(It.Is<string>(s => s.Contains("\"Rooms\":[]"))), Times.Once); 
            _mockLogger.Verify(
                x => x.Log(
                    Microsoft.Extensions.Logging.LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"player({Player1Id}) connected")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Test]
        public void Connect_WhenUserNotFound_ThrowsInvalidDataException()
        {
            // Arrange
            var mockConn = SetupMockConnection(999); 
            _mockUserService.Setup(us => us.FindUser(999)).ReturnsAsync((User?)null); 


            // Act, Assert
            var ex = Assert.ThrowsAsync<InvalidDataException>(async () => await _multiplayerService.Connect(999, mockConn.Object));
            Assert.That(ex.Message, Does.Contain("User with ID 999 not found."));
        }

        [Test]
        public async Task CreateRoom_WhenPlayerConnected_CreatesRoomAndNotifiesPlayerAndBroadcasts()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            await ConnectPlayer(Player2Id); 

            var roomName = "Test Room";
            var visibility = GameVisibility.PUBLIC;
            var allowedPlayers = new HashSet<int>();

            // Act
            _multiplayerService.CreateRoom(Player1Id, roomName, visibility, allowedPlayers);

            var rooms = _multiplayerService.GetJoinableRooms(Player1Id);

            // Assert
            Assert.AreEqual(1, rooms.Count);
            Assert.AreEqual(roomName, rooms[0].Name);
            Assert.AreEqual(Player1Id, rooms[0].CreatorId);
            Assert.Contains(Player1Name, rooms[0].Players);
            _mockConnections[Player1Id].Verify(ws => ws.Send(It.Is<string>(s => s.Contains($"\"Name\":\"{roomName}\"") && s.Contains($"\"CreatorId\":{Player1Id}"))), Times.Exactly(2));
            _mockConnections[Player2Id].Verify(ws => ws.Send(It.Is<string>(s => s.Contains("\"Rooms\":") && s.Contains(roomName))), Times.AtLeastOnce);
        }

        [Test]
        public async Task CreateRoom_WhenPlayerAlreadyInARoom_ThrowsInvalidOperationException()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            await ConnectPlayer(Player2Id);
            _multiplayerService.CreateRoom(Player1Id, "Room 1", GameVisibility.PUBLIC, new HashSet<int>());
            var rooms = _multiplayerService.GetJoinableRooms(Player2Id);
            Guid room1Id = rooms[0].Id;
            _multiplayerService.JoinRoom(Player2Id, room1Id); 

            // Act, Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _multiplayerService.CreateRoom(Player2Id, "Room 2", GameVisibility.PUBLIC, new HashSet<int>()));
            Assert.That(ex.Message, Does.Contain($"User {Player2Id} is already in the room"));
        }


        [Test]
        public async Task JoinRoom_WhenValid_AddsPlayerAndBroadcastsUpdate()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            await ConnectPlayer(Player2Id);
            _multiplayerService.CreateRoom(Player1Id, "Join Test", GameVisibility.PUBLIC, new HashSet<int>());
            Guid roomId = _multiplayerService.GetJoinableRooms(Player2Id).First().Id;

            _mockConnections[Player1Id].Invocations.Clear();
            _mockConnections[Player2Id].Invocations.Clear();

            // Act
            _multiplayerService.JoinRoom(Player2Id, roomId);

            // Assert
            var roomAfterJoin = _multiplayerService.GetJoinableRooms(Player1Id).FirstOrDefault(r => r.Id == roomId);

            _mockConnections[Player1Id].Verify(ws => ws.Send(It.Is<string>(s => s.Contains(roomId.ToString()) && s.Contains(Player1Name) && s.Contains(Player2Name) && s.Contains(RoomStatus.WAITING.ToString()))), Times.Once);
            _mockConnections[Player2Id].Verify(ws => ws.Send(It.Is<string>(s => s.Contains(roomId.ToString()) && s.Contains(Player1Name) && s.Contains(Player2Name) && s.Contains(RoomStatus.WAITING.ToString()))), Times.Once);
        }


        [Test]
        public async Task JoinRoom_WhenRoomNotFound_ThrowsInvalidDataException()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            Guid fakeRoomId = Guid.NewGuid();

            // Act Assert
            var ex = Assert.Throws<InvalidDataException>(() => _multiplayerService.JoinRoom(Player1Id, fakeRoomId));
            Assert.That(ex.Message, Does.Contain($"User {Player1Id} Or room {fakeRoomId} not found"));
        }

        [Test]
        public async Task JoinRoom_WhenPlayerAlreadyInAnotherRoom_ThrowsInvalidOperationException()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            await ConnectPlayer(Player2Id);
            _multiplayerService.CreateRoom(Player1Id, "Room 1", GameVisibility.PUBLIC, new HashSet<int>());
            _multiplayerService.CreateRoom(Player2Id, "Room 2", GameVisibility.PUBLIC, new HashSet<int>());
            Guid room1Id = _multiplayerService.GetJoinableRooms(Player1Id).First(r => r.Name == "Room 1").Id;

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _multiplayerService.JoinRoom(Player2Id, room1Id));
            Assert.That(ex.Message, Does.Contain($"User {Player2Id} is already in the room"));
        }

        [Test]
        public async Task StartRoom_WhenCalledByCreatorWithEnoughPlayers_ChangesStatusAndStartsRound()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            await ConnectPlayer(Player2Id);
            _multiplayerService.CreateRoom(Player1Id, "Start Test", GameVisibility.PUBLIC, new HashSet<int>());
            Guid roomId = _multiplayerService.GetJoinableRooms(Player2Id).First().Id;
            _multiplayerService.JoinRoom(Player2Id, roomId);

            _mockConnections[Player1Id].Invocations.Clear();
            _mockConnections[Player2Id].Invocations.Clear();

            // Act
            _multiplayerService.StartRoom(Player1Id, roomId);

            // Assert
            _mockConnections[Player1Id].Verify(ws => ws.Send(It.Is<string>(s => s.Contains("\"X\":") && s.Contains("\"Y\":"))), Times.Once); 
            _mockConnections[Player2Id].Verify(ws => ws.Send(It.Is<string>(s => s.Contains("\"X\":") && s.Contains("\"Y\":"))), Times.Once); 
            _mockLogger.Verify(
               x => x.Log(
                   Microsoft.Extensions.Logging.LogLevel.Information,
                   It.IsAny<EventId>(),
                   It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"round of room({roomId}) started")),
                   null,
                   It.IsAny<Func<It.IsAnyType, Exception, string>>()),
               Times.Once);
        }

        [Test]
        public async Task StartRoom_WhenRoomNotFound_ThrowsInvalidDataException()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            Guid fakeRoomId = Guid.NewGuid();

            // Act, Assert
            var ex = Assert.Throws<InvalidDataException>(() => _multiplayerService.StartRoom(Player1Id, fakeRoomId));
            Assert.That(ex.Message, Does.Contain($"Room {fakeRoomId} not found"));
        }

        [Test]
        public async Task StartRoom_WhenNotCalledByCreator_ThrowsInvalidOperationException()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            await ConnectPlayer(Player2Id);
            _multiplayerService.CreateRoom(Player1Id, "Start Test", GameVisibility.PUBLIC, new HashSet<int>());
            Guid roomId = _multiplayerService.GetJoinableRooms(Player2Id).First().Id;
            _multiplayerService.JoinRoom(Player2Id, roomId);

            // Act,  Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _multiplayerService.StartRoom(Player2Id, roomId)); // Player 2 tries to start
            Assert.That(ex.Message, Does.Contain($"User {Player2Id} not allowed to start."));
        }

        [Test]
        public async Task StartRoom_WhenNotEnoughPlayers_ThrowsInvalidOperationException()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            _multiplayerService.CreateRoom(Player1Id, "Start Test", GameVisibility.PUBLIC, new HashSet<int>());
            Guid roomId = _multiplayerService.GetJoinableRooms(Player1Id).First().Id;

            // Act, Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _multiplayerService.StartRoom(Player1Id, roomId));
            Assert.That(ex.Message, Does.Contain($"Minimum 2 players required(Room {roomId})."));
        }


        [Test]
        public async Task RegisterTargetHit_WhenValid_RegistersTime()
        {
            // Arrange 
            await ConnectPlayer(Player1Id);
            await ConnectPlayer(Player2Id);
            _multiplayerService.CreateRoom(Player1Id, "Hit Test", GameVisibility.PUBLIC, new HashSet<int>());
            Guid roomId = _multiplayerService.GetJoinableRooms(Player2Id).First().Id;
            _multiplayerService.JoinRoom(Player2Id, roomId);
            _multiplayerService.StartRoom(Player1Id, roomId); 

            double reactionTime = 0.5;

            // Act
            _multiplayerService.RegisterTargetHit(Player2Id, roomId, reactionTime);

            // Assert
            _mockLogger.Verify(
               x => x.Log(
                   Microsoft.Extensions.Logging.LogLevel.Information,
                   It.IsAny<EventId>(),
                   It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"registered hit for player({Player2Id}) in room({roomId})")),
                   null,
                   It.IsAny<Func<It.IsAnyType, Exception, string>>()),
               Times.Once);
        }

        [Test]
        public async Task RegisterTargetHit_WhenRoomNotPlaying_ThrowsInvalidOperationException()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            await ConnectPlayer(Player2Id);
            _multiplayerService.CreateRoom(Player1Id, "Hit Test", GameVisibility.PUBLIC, new HashSet<int>());
            Guid roomId = _multiplayerService.GetJoinableRooms(Player2Id).First().Id;
            _multiplayerService.JoinRoom(Player2Id, roomId);
            double reactionTime = 0.5;

            // Act,  Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _multiplayerService.RegisterTargetHit(Player2Id, roomId, reactionTime));
            Assert.That(ex.Message, Does.Contain($"Room {roomId} is not in a playing state."));
        }

        [Test]
        public async Task RegisterTargetHit_WhenPlayerNotInRoom_ThrowsInvalidDataException()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            await ConnectPlayer(Player2Id);
            await ConnectPlayer(Player3Id);
            _multiplayerService.CreateRoom(Player1Id, "Hit Test", GameVisibility.PUBLIC, new HashSet<int>());
            Guid roomId = _multiplayerService.GetJoinableRooms(Player2Id).First().Id;
            _multiplayerService.JoinRoom(Player2Id, roomId);
            _multiplayerService.StartRoom(Player1Id, roomId);

            double reactionTime = 0.5;

            // Act,  Assert
            var ex = Assert.Throws<InvalidDataException>(() => _multiplayerService.RegisterTargetHit(Player3Id, roomId, reactionTime)); // Player 3 tries to hit
            Assert.That(ex.Message, Does.Contain($"User {Player3Id} not in room Or room {roomId} not found"));
        }

        [Test]
        public async Task Disconnect_WhenPlayerInRoom_RemovesPlayerFromServiceAndRoomAndClosesConnection()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            await ConnectPlayer(Player2Id);
            _multiplayerService.CreateRoom(Player1Id, "Disconnect Test", GameVisibility.PUBLIC, new HashSet<int>());
            Guid roomId = _multiplayerService.GetJoinableRooms(Player2Id).First().Id;
            _multiplayerService.JoinRoom(Player2Id, roomId);

            // Act
            _multiplayerService.Disconnect(Player2Id);

            // Assert
            _mockConnections[Player2Id].Verify(ws => ws.Close(), Times.Once);

            var roomsForP1 = _multiplayerService.GetJoinableRooms(Player1Id); 
            Assert.AreEqual(1, roomsForP1.Count);
            Assert.AreEqual(1, roomsForP1[0].Players.Count); 
            Assert.IsFalse(roomsForP1[0].Players.Contains(Player2Name));

            var ex = Assert.Throws<InvalidDataException>(() => _multiplayerService.CreateRoom(Player2Id, "New Room", GameVisibility.PUBLIC, new HashSet<int>()));
            Assert.That(ex.Message, Does.Contain($"User {Player2Id} not found"));

        }

        [Test]
        public async Task Disconnect_WhenLastPlayerInRoom_RemovesRoomAndClosesConnection()
        {
            // Arrange
            await ConnectPlayer(Player1Id);
            _multiplayerService.CreateRoom(Player1Id, "Last Player Test", GameVisibility.PUBLIC, new HashSet<int>());
            Guid roomId = _multiplayerService.GetJoinableRooms(Player1Id).First().Id;

            // Act
            _multiplayerService.Disconnect(Player1Id);

            // Assert
            _mockConnections[Player1Id].Verify(ws => ws.Close(), Times.Once);

            await ConnectPlayer(Player2Id);
            var roomsForP2 = _multiplayerService.GetJoinableRooms(Player2Id);
            Assert.IsEmpty(roomsForP2); 
        }

    }
}