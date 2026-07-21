using Microsoft.Extensions.Options;
using Minio;
using SocialService.Application.Clients;
using SocialService.Application.Interfaces;
using SocialService.Application.Settings;
using SocialService.Application.Storages;

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
    
    public static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioSettings>(configuration.GetSection(MinioSettings.SectionName));

        services.AddSingleton<IMinioClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MinioSettings>>().Value;

            return new MinioClient()
                .WithEndpoint(settings.Endpoint)
                .WithCredentials(settings.AccessKey, settings.SecretKey)
                .WithSSL(settings.UseSSL)
                .Build();
        });
        
        services.AddScoped<IAvatarStorage, MinioAvatarStorage>();
        
        return services;
    }
    
}