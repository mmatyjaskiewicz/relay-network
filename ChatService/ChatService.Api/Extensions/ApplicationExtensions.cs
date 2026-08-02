namespace ChatService.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddJwtAuthentication(configuration);
        services.AddExceptionHandling();
        services.AddMassTransitServices(configuration);
        
        return services;
    }
}