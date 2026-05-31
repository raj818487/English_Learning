namespace EnglishLearning.Domain.Exceptions;

public sealed class ContentValidationException(string message) : Exception(message);
