namespace EnglishLearning.API.Models;

public sealed class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public IReadOnlyList<string>? Errors { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResponse<T> Ok(T data, string message = "Operation completed successfully") =>
        new() { Success = true, Message = message, Data = data };

    public static ApiResponse<T> Fail(string message, params string[] errors) =>
        new() { Success = false, Message = message, Errors = errors };
}
