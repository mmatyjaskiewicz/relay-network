using AuthService.Application.Exceptions;
using AuthService.Application.Exceptions.BadRequest;
using AuthService.Application.Exceptions.Conflict;
using AuthService.Application.Exceptions.NotFound;
using AuthService.Application.Exceptions.Unauthorized;
using Microsoft.AspNetCore.Diagnostics;

namespace AuthService.Api.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            ConflictException => StatusCodes.Status409Conflict,
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
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
            success = false,
            error = message
        }, cancellationToken);

        return true;
    }
}