using MassTransit;

namespace AuthService.Api.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
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
            });
        });

        return services;
    }
}