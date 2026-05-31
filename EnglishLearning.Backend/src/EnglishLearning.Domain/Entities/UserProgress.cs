namespace EnglishLearning.Domain.Entities;

public class UserProgress
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid ContentId { get; set; }
    public bool Completed { get; set; }
    public int Score { get; set; }
    public int LearningStreakDays { get; set; }
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}
