using AuthService.Application.Interfaces;
using AuthService.Application.Persistence;
using AuthService.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Api.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}