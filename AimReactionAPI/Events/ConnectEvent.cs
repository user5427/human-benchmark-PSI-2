using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;


public class ConnectEventDto : BaseDto
{
    public int PlayerId { get; set; }
}

public class ConnectEvent : BaseEventHandler<ConnectEventDto>
{
    private readonly MultiplayerService _multiplayerService;

    public ConnectEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override async Task Handle(ConnectEventDto dto, IWebSocketConnection socket)
    {
        await _multiplayerService.Connect(dto.PlayerId, socket);
        return;
    }
}