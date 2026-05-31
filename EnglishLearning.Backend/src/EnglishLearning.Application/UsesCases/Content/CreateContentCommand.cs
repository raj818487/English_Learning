namespace EnglishLearning.Application.UsesCases.Content;

public sealed record CreateContentCommand(string Title, string Text, Guid TopicId);
