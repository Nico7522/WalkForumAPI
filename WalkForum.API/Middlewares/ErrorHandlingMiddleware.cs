
using System.Net.Mime;
using WalkForum.Domain.Exceptions;

namespace WalkForum.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
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
            logger.LogInformation("Catch Bad Request");
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(e.Message);   
        }
   
        catch (ValidationException e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(e.Message);
        }

    }
}
