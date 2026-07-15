using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using SocialService.Application.Exceptions;
using SocialService.Application.Exceptions.Conflict;
using SocialService.Application.Exceptions.Forbidden;
using SocialService.Application.Exceptions.NotFound;

namespace SocialService.Api.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var title = exception switch
        {
            ConflictException => "Conflict",
            ForbiddenException => "Forbidden",
            NotFoundException => "Not Found",
            ValidationException => "Validation failed",
            _ => "Internal Server Error"
        };
        
        var statusCode = exception switch
        {
            ConflictException => StatusCodes.Status409Conflict,
            ForbiddenException => StatusCodes.Status403Forbidden,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        var message = exception switch
        {
            AppException => exception.Message,
            _ => "Internal server error"
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(new
        {
            Title = title,
            Status = statusCode,
            error = message
        }, cancellationToken);

        return true;
    }
}