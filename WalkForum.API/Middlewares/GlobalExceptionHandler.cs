using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using WalkForum.Domain.Exceptions;

namespace WalkForum.API.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        if (exception is UnauthorizedException) {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(exception.Message));
            return true;
        }

        if (exception is BadRequestException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(exception.Message));
            return true;
        }

        if (exception is NotFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(exception.Message));

        }
            return false;
    }
}
