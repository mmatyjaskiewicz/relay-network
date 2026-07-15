using AuthService.Api.Filters;

namespace AuthService.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddJwtAuthentication(configuration);
        services.AddPersistence(configuration);
        services.AddValidation();
        services.AddExceptionHandling();
        services.AddMassTransitServices(configuration);
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        }); 

        return services;
    }
}