namespace EnglishLearning.API.Middleware;

public sealed class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var method = context.Request.Method.ReplaceLineEndings(string.Empty).Trim();
        var path = (context.Request.Path.Value ?? "/").ReplaceLineEndings(string.Empty).Trim();
        logger.LogInformation("Handling request {Method} {Path}", method, path);
        await next(context);
    }
}
