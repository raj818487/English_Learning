namespace EnglishLearning.Domain.Entities;

public class ContentHash
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Hash { get; set; } = string.Empty;
    public Guid ContentId { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
