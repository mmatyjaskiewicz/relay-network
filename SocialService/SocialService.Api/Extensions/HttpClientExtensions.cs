using Microsoft.Extensions.Options;
using SocialService.Application.Clients;
using SocialService.Application.Settings;

namespace SocialService.Api.Extensions;

public static class HttpClientsExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServiceUrls>(configuration.GetSection("ServiceUrls"));

        services.AddHttpClient<AuthClient>((provider, client) =>
        {
            var serviceUrls = provider
                .GetRequiredService<IOptions<ServiceUrls>>()
                .Value;

            client.BaseAddress = new Uri(serviceUrls.Auth);
        });

        return services;
    }
}