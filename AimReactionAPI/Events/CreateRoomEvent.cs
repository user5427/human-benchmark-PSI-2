using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;


public class CreateRoomEventDto : BaseDto
{
    public int PlayerId { get; set; }
    public required string RoomName { get; set; }
}

public class CreateRoomEvent : BaseEventHandler<CreateRoomEventDto>
{
    private readonly MultiplayerService _multiplayerService;

    public CreateRoomEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(CreateRoomEventDto dto, IWebSocketConnection socket)
    {
        _multiplayerService.CreateRoom(dto.PlayerId, dto.RoomName);
        return Task.CompletedTask;
    }
}