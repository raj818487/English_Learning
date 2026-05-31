namespace EnglishLearning.Application.UsesCases.Interview;

public sealed record CreateInterviewQuestionCommand(string Title, string Text, Guid TopicId);
