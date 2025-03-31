using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;

public class StartRoomEvent : BaseEventHandler<StartRoomRequest>
{
    private readonly MultiplayerService _multiplayerService;

    public StartRoomEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(StartRoomRequest dto, IWebSocketConnection socket)
    {
        _multiplayerService.StartRoom(dto.PlayerId, dto.RoomId);
        return Task.CompletedTask;
    }
}