using System.Net;
using System.Text.Json;
using EnglishLearning.API.Models;
using EnglishLearning.Domain.Exceptions;

namespace EnglishLearning.API.Middleware;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DuplicateContentException ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.Conflict, ex.Message);
        }
        catch (ContentValidationException ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    private static async Task WriteErrorAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = ApiResponse<object>.Fail("Operation failed", message);
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
