using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.ValueObjects;

public readonly record struct ContentType(ContentTypeEnum Value)
{
    public override string ToString() => Value.ToString();
}
