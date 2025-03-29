using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;


public class StartRoomEventDto : BaseDto
{
    public int PlayerId { get; set; }
    public required Guid RoomId { get; set; }
}

public class StartRoomEvent : BaseEventHandler<StartRoomEventDto>
{
    private readonly MultiplayerService _multiplayerService;

    public StartRoomEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(StartRoomEventDto dto, IWebSocketConnection socket)
    {
        _multiplayerService.StartRoom(dto.PlayerId, dto.RoomId);
        return Task.CompletedTask;
    }
}