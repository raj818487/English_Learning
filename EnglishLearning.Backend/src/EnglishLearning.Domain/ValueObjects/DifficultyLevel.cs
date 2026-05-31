using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.ValueObjects;

public readonly record struct DifficultyLevel(DifficultyEnum Value)
{
    public override string ToString() => Value.ToString();
}
