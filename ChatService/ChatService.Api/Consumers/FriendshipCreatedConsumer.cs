using MassTransit;
using Shared.Contracts.Events;

namespace ChatService.Api.Consumers;

public class FriendshipCreatedConsumer() : IConsumer<FriendshipCreated>
{
    public async Task Consume(ConsumeContext<FriendshipCreated> context)
    {
        var message = context.Message;
        Console.WriteLine($"Friendship created between {message.UserId} and {message.FriendId}");
    }
}