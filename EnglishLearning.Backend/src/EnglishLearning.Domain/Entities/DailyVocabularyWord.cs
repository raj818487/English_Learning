namespace EnglishLearning.Domain.Entities;

public sealed class DailyVocabularyWord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Word { get; set; } = string.Empty;
    public DateOnly GeneratedOnDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
