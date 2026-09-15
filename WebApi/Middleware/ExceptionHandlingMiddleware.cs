using System.Text.Json;
using Utilities.Models.Responses.Generic;

namespace WebApi.Middleware;

internal class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment environment) : IMiddleware
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex) when (context.RequestAborted.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(ex, "Request aborted by the client for {Method} {Path}.", context.Request.Method, context.Request.Path);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred for {Method} {Path}.", context.Request.Method, context.Request.Path);

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var message = environment.IsDevelopment()
            ? exception.Message
            : "An unexpected error occurred. Please try again later.";

        var ressponseBody = new ErrorResultResponse { Errors = [message] };

        await context.Response.WriteAsync(JsonSerializer.Serialize(
            new InternalServerErrorResponse(ressponseBody),
            _jsonOptions
        ));
    }
}
