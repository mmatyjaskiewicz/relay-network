namespace AuthService.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddServices();
        services.AddJwtAuthentication(configuration);
        services.AddPersistence(configuration);
        services.AddValidation();
        services.AddExceptionHandling();
        services.AddMassTransitServices(configuration);

        return services;
    }
}