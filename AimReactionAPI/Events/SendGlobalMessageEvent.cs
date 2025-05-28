using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;

public class SendGlobalMessageEvent : BaseEventHandler<GlobalMessageRequest>
{
    private readonly IServiceScopeFactory _scopeFactory;
    public SendGlobalMessageEvent(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public override async Task Handle(GlobalMessageRequest dto, IWebSocketConnection socket)
    {
        using var scope = _scopeFactory.CreateScope();
        var messageService = scope.ServiceProvider.GetRequiredService<MessageService>();
        await messageService.SendGlobalMessage(dto);
    }
}