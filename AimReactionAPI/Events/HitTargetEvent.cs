using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;


public class HitTargetEventEventDto : BaseDto
{
    public int PlayerId { get; set; }
    public required Guid RoomId { get; set; }
    public double ReactionTime { get; set; }
}

public class HitTargetEvent : BaseEventHandler<HitTargetEventEventDto>
{
    private readonly MultiplayerService _multiplayerService;

    public HitTargetEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(HitTargetEventEventDto dto, IWebSocketConnection socket)
    {
        _multiplayerService.RegisterTargetHit(dto.PlayerId, dto.RoomId, dto.ReactionTime);
        return Task.CompletedTask;
    }
}