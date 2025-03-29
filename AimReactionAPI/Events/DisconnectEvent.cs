using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;


public class DisconnectEventDto : BaseDto
{
    public int PlayerId { get; set; }
}

public class DisconnectEvent : BaseEventHandler<DisconnectEventDto>
{
    private readonly MultiplayerService _multiplayerService;

    public DisconnectEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(DisconnectEventDto dto, IWebSocketConnection socket)
    {
        _multiplayerService.Disconnect(dto.PlayerId);
        return Task.CompletedTask;
    }
}