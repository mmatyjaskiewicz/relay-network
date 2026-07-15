using Microsoft.EntityFrameworkCore;
using SocialService.Application.Interfaces;
using SocialService.Application.Persistence;
using SocialService.Application.Repositories;

namespace SocialService.Api.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SocialDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        
        services.AddScoped<IFriendshipRepository, FriendshipRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();

        return services;
    }
}