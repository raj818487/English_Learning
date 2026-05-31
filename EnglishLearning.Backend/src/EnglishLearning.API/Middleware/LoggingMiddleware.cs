namespace EnglishLearning.API.Middleware;

public sealed class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        logger.LogInformation("Handling request {Method} {Path}", context.Request.Method, context.Request.Path);
        await next(context);
    }
}
