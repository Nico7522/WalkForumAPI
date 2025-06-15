using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WalkForum.Domain.Exceptions;

namespace WalkForum.API.Middlewares;

internal sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        httpContext.Response.StatusCode = exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        if (exception is UnauthorizedException) {
            var problemDetails = new ProblemDetails { Status = StatusCodes.Status401Unauthorized, Title = "Unauthorized", Detail = exception.Message, Type = "Unauthorized" };
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext { HttpContext = httpContext, ProblemDetails = problemDetails });
        }

        if (exception is ForbiddenException)
        {
            var problemDetails = new ProblemDetails { Status = StatusCodes.Status403Forbidden, Title = "You can not access to this ressource", Detail = exception.Message, Type = "Unauthorized" };
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext { HttpContext = httpContext, ProblemDetails = problemDetails });
        }

        if (exception is BadRequestException)
        {
            var problemDetails = new ProblemDetails { Status = StatusCodes.Status400BadRequest, Title = "Bad Request", Detail = exception.Message, Type = "Bad Request" };
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext { HttpContext = httpContext, ProblemDetails = problemDetails });
        }

        if (exception is NotFoundException)
        {
            var problemDetails = new ProblemDetails { Status = StatusCodes.Status404NotFound, Title = "Ressource not found", Detail = exception.Message, Type = "Not Found" };
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext { HttpContext = httpContext, ProblemDetails = problemDetails});
       
        }

        if(exception is ValidationException)
        {
            var ex = (ValidationException)exception;
            var errors = JsonSerializer.Serialize(ex.Errors);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = exception.Message,
                Type = "Validation Errors",
                Extensions = new Dictionary<string, object?> { { "details", ex.Errors } }
            };
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext { HttpContext = httpContext, ProblemDetails = problemDetails});
        }
        if(exception is Exception)
        {
            Console.WriteLine(exception.Message);
        }

            return false;
    }
}
