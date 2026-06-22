using AuthService.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace AuthService.Api.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            UserAlreadyExistsException => StatusCodes.Status409Conflict,
            
            InvalidCredentialsException => StatusCodes.Status401Unauthorized,
            
            _ => StatusCodes.Status500InternalServerError
        };

        var message = exception switch
        {
            UserAlreadyExistsException => exception.Message, 
            
            InvalidCredentialsException => exception.Message,
            
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