using AuthService.Api.Filters;
using AuthService.Application.Validators;
using FluentValidation;

namespace AuthService.Api.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        services.AddScoped<ValidationFilter>();
        
        return services;
    }
}