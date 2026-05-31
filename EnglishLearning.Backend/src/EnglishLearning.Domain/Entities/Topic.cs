using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.Entities;

public class Topic
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public TopicCategoryEnum Category { get; set; } = TopicCategoryEnum.General;
    public DifficultyEnum Difficulty { get; set; } = DifficultyEnum.Beginner;
    public ICollection<Content> Contents { get; set; } = new List<Content>();
}
