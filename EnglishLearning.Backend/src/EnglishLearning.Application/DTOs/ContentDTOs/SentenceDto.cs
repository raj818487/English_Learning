using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs.ContentDTOs;

public sealed class SentenceDto
{
    public Guid TopicId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DifficultyEnum Difficulty { get; set; } = DifficultyEnum.Beginner;
}
