
using System.Net.Mime;
using System.Text.Json;
using WalkForum.Domain.Exceptions;

namespace WalkForum.API.Middlewares;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            logger.LogInformation("Try");
            await next.Invoke(context);
        }
        catch(NotFoundException e)
        {
            logger.LogInformation("Catch Not  Found exception");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(e.Message);   
        }
        catch (BadRequestException e)
        {
            logger.LogInformation("Catch Bad Request exception");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(e.Message);
        }

        catch (ValidationException e)
        {
            logger.LogInformation("Catch Validation exception");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { Message = e.Message, Erros = e.Errors }));
        }
        catch(UnauthorizedException e)
        {
            logger.LogInformation("Catch Unauthorized exception");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync(JsonSerializer.Serialize(e.Message));
        }

    }
}
