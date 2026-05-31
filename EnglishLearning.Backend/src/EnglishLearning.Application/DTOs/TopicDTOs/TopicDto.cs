using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs.TopicDTOs;

public sealed class TopicDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TopicCategoryEnum Category { get; set; } = TopicCategoryEnum.General;
    public DifficultyEnum Difficulty { get; set; } = DifficultyEnum.Beginner;
}
