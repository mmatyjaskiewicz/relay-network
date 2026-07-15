using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthService.Api.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null)
                continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());

            var validator = context.HttpContext.RequestServices.GetService(validatorType);

            if (validator is not IValidator nonGenericValidator)
                continue;

            ValidationResult result = await nonGenericValidator.ValidateAsync(new ValidationContext<object>(argument),
                context.HttpContext.RequestAborted);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(x => x.ErrorMessage).ToArray());

                context.Result = new BadRequestObjectResult(new
                {
                    Title = "Validation failed",
                    Status = StatusCodes.Status400BadRequest,
                    Errors = errors
                });

                return;
            }
        }

        await next();
    }
}