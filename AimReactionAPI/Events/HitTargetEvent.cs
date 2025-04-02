using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;

public class HitTargetEvent : BaseEventHandler<HitTargetRequest>
{
    private readonly MultiplayerService _multiplayerService;

    public HitTargetEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(HitTargetRequest dto, IWebSocketConnection socket)
    {
        _multiplayerService.RegisterTargetHit(dto.PlayerId, dto.RoomId, dto.ReactionTime);
        return Task.CompletedTask;
    }
}