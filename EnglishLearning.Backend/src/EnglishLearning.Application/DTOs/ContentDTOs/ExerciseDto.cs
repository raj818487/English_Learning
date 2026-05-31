using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs.ContentDTOs;

public sealed class ExerciseDto
{
    public Guid TopicId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public ExerciseTypeEnum ExerciseType { get; set; } = ExerciseTypeEnum.MultipleChoice;
    public DifficultyEnum Difficulty { get; set; } = DifficultyEnum.Beginner;
}
