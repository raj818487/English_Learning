namespace EnglishLearning.Application.DTOs.ContentDTOs;

public sealed class DailyVocabularyWordDto
{
    public Guid Id { get; set; }
    public string Word { get; set; } = string.Empty;
    public DateOnly GeneratedOnDate { get; set; }
}
