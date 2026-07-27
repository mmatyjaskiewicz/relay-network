using ChatService.Api.Exceptions;

namespace ChatService.Api.Extensions;

public static class ExceptionsExtensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}