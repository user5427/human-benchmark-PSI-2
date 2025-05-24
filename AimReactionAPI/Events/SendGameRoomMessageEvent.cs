using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;

public class SendGameRoomMessageEvent : BaseEventHandler<GameRoomMessageRequest>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SendGameRoomMessageEvent(IServiceScopeFactory scopeFactory)
    {
       _scopeFactory = scopeFactory;
    }
    public override async Task Handle(GameRoomMessageRequest dto, IWebSocketConnection socket)
    {
        using var scope = _scopeFactory.CreateScope();
        var messageService = scope.ServiceProvider.GetRequiredService<MessageService>();
        await messageService.SendGameRoomMessage(dto);
    }
}