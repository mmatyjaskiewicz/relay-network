using FluentValidation;
using SocialService.Api.Filters;
using SocialService.Application.Validators;

namespace SocialService.Api.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AcceptFriendRequestValidator>();
        services.AddScoped<ValidationFilter>();

        return services;
    }
}