using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SocialService.Api.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null)
                continue;

            var dtoType = argument.GetType();

            var validatorType = typeof(IValidator<>).MakeGenericType(dtoType);

            var validator = context.HttpContext.RequestServices.GetService(validatorType);

            if (validator is null)
                continue;

            var validationContextType = typeof(ValidationContext<>).MakeGenericType(dtoType);

            var validationContext = Activator.CreateInstance(validationContextType, argument);

            var validateAsyncMethod = validatorType.GetMethod(nameof(IValidator.ValidateAsync));

            var validationTask = (Task<ValidationResult>)validateAsyncMethod!
                .Invoke(validator, new[] { validationContext!, context.HttpContext.RequestAborted })!;

            var validationResult = await validationTask;

            if (!validationResult.IsValid)
            {
                context.Result = new BadRequestObjectResult(validationResult.Errors);

                return;
            }
        }

        await next();
    }
}