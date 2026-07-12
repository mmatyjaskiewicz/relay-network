using SocialService.Api.Exceptions;

namespace SocialService.Api.Extensions;

public static class ExceptionExtensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}