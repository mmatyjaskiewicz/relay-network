using MassTransit;
using SocialService.Api.Consumers;

namespace SocialService.Api.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserRegisteredConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(
                    configuration["RabbitMQ:Host"]!,
                    "/",
                    host =>
                    {
                        host.Username(configuration["RabbitMQ:Username"]!);
                        host.Password(configuration["RabbitMQ:Password"]!);
                    });
                
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}