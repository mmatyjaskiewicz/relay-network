using SocialService.Application.Services;

namespace SocialService.Api.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<FriendshipService>();
        services.AddScoped<ProfileService>();
        
        return services;
    }
}