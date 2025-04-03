using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;

public class CreateRoomEvent : BaseEventHandler<CreateRoomRequest>
{
    private readonly MultiplayerService _multiplayerService;

    public CreateRoomEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(CreateRoomRequest dto, IWebSocketConnection socket)
    {
        _multiplayerService.CreateRoom(dto.PlayerId, dto.RoomName, dto.Visibility, dto.AllowedPlayers);
        return Task.CompletedTask;
    }
}

