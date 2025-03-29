using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;


public class JoinRoomEventDto : BaseDto
{
    public int PlayerId { get; set; }
    public required Guid RoomId { get; set; }
}

public class JoinRoomEvent : BaseEventHandler<JoinRoomEventDto>
{
    private readonly MultiplayerService _multiplayerService;

    public JoinRoomEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(JoinRoomEventDto dto, IWebSocketConnection socket)
    {
        _multiplayerService.JoinRoom(dto.PlayerId, dto.RoomId);
        return Task.CompletedTask;
    }
}