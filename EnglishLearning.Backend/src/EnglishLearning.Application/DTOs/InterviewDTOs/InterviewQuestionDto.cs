using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs.InterviewDTOs;

public sealed class InterviewQuestionDto
{
    public Guid TopicId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string SuggestedAnswer { get; set; } = string.Empty;
    public DifficultyEnum Difficulty { get; set; } = DifficultyEnum.Intermediate;
}
