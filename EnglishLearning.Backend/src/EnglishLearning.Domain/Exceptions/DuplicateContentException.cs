namespace EnglishLearning.Domain.Exceptions;

public sealed class DuplicateContentException(string message) : Exception(message);
