using ChatService.Application.Exceptions;
using ChatService.Application.Exceptions.Unauthorized;
using Microsoft.AspNetCore.Diagnostics;

namespace ChatService.Api.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var title = exception switch
        {
            UnauthorizedException => "Unauthorized",
            _ => "Internal Server Error"
        };
        
        var statusCode = exception switch
        {
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
            Title = title,
            Status = statusCode,
            error = message
        }, cancellationToken);

        return true;
    }
}