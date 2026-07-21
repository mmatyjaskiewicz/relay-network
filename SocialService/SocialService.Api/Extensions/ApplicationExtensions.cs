using SocialService.Api.Filters;

namespace SocialService.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddJwtAuthentication(configuration);
        services.AddHttpClients(configuration);
        services.AddExceptionHandling();
        services.AddServices();
        services.AddValidation();
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
        services.AddMassTransitServices(configuration);
        services.AddMinio(configuration);
        
        return services;
    }
}