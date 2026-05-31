namespace EnglishLearning.API.Middleware;

public sealed class ValidationMiddleware(RequestDelegate next)
{
    public Task InvokeAsync(HttpContext context) => next(context);
}
