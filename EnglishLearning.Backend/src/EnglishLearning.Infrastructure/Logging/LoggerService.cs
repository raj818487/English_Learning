using Microsoft.Extensions.Logging;

namespace EnglishLearning.Infrastructure.Logging;

public sealed class LoggerService(ILogger<LoggerService> logger)
{
    public void LogInformation(string message) => logger.LogInformation("{Message}", message);
}
