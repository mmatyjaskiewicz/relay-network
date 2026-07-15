using MassTransit;
using Shared.Contracts.Events;
using SocialService.Application.Services;

namespace SocialService.Api.Consumers;

public class UserRegisteredConsumer(ProfileService profileService) : IConsumer<UserRegistered>
{
    public async Task Consume(ConsumeContext<UserRegistered> context)
    {
        await profileService.CreateProfileAsync(context.Message.UserId, context.Message.Username);
    }
}