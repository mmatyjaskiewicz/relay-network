using AuthService.Application.Security;
using AuthService.Application.Services;

namespace AuthService.Api.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationService>();
        services.AddScoped<UserService>();
        services.AddSingleton<JwtGenerator>();
        
        return services;
    }
}