using FluentValidation;
using IMS.Domain.Exceptions;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using UnauthorizedAccessException = System.UnauthorizedAccessException;

namespace IMS.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong: {Ex}", ex);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest; // Bad request for validation errors

            var errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                context.Response.StatusCode,
                Message = "Validation errors occurred",
                Errors = errors
            }));
        }

        context.Response.StatusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            ForbiddenAccessException => StatusCodes.Status403Forbidden,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            ConflictException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message // Consider more generic message for production
        }));
    }
}

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;

    public override string ToString() => JsonSerializer.Serialize(this);
}