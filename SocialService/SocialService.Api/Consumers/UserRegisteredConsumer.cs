using MassTransit;
using Shared.Contracts.Events;

namespace SocialService.Api.Consumers;

public class UserRegisteredConsumer : IConsumer<UserRegistered>
{
    public async Task Consume(ConsumeContext<UserRegistered> context)
    {
        Console.WriteLine($"Hello {context.Message.Username}! Your id is {context.Message.UserId}");

        await Task.CompletedTask;
    }
}