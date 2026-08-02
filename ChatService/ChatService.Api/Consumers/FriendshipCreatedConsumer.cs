using ChatService.Application.Services;
using MassTransit;
using Shared.Contracts.Events;

namespace ChatService.Api.Consumers;

public class FriendshipCreatedConsumer(ChatManager chatManager) : IConsumer<FriendshipCreated>
{
    public async Task Consume(ConsumeContext<FriendshipCreated> context)
    {
        await chatManager.CreatePrivateChatAsync(context.Message.UserId, context.Message.FriendId);
    }
}