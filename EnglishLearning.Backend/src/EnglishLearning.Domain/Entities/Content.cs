using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.Entities;

public abstract class Content
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TopicId { get; set; }
    public Topic? Topic { get; set; }
    public ContentTypeEnum ContentType { get; protected set; }
    public DifficultyEnum Difficulty { get; set; } = DifficultyEnum.Beginner;
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string MetadataJson { get; set; } = "{}";
    public string ContentHash { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

public class Vocabulary : Content
{
    public Vocabulary() => ContentType = ContentTypeEnum.Vocabulary;
    public string Definition { get; set; } = string.Empty;
}

public class GrammarRule : Content
{
    public GrammarRule() => ContentType = ContentTypeEnum.GrammarRule;
}

public class Sentence : Content
{
    public Sentence() => ContentType = ContentTypeEnum.Sentence;
}

public class Exercise : Content
{
    public Exercise() => ContentType = ContentTypeEnum.Exercise;
    public ExerciseTypeEnum ExerciseType { get; set; } = ExerciseTypeEnum.MultipleChoice;
    public string CorrectAnswer { get; set; } = string.Empty;
}

public class InterviewQuestion : Content
{
    public InterviewQuestion() => ContentType = ContentTypeEnum.InterviewQuestion;
    public string SuggestedAnswer { get; set; } = string.Empty;
}

public class ConversationScenario : Content
{
    public ConversationScenario() => ContentType = ContentTypeEnum.ConversationScenario;
}
