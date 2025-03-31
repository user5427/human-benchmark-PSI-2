using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;


public class JoinRoomEvent : BaseEventHandler<JoinRoomRequest>
{
    private readonly MultiplayerService _multiplayerService;

    public JoinRoomEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(JoinRoomRequest dto, IWebSocketConnection socket)
    {
        _multiplayerService.JoinRoom(dto.PlayerId, dto.RoomId);
        return Task.CompletedTask;
    }
}