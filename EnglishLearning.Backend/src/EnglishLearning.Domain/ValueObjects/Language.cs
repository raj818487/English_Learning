namespace EnglishLearning.Domain.ValueObjects;

public readonly record struct Language(string Value)
{
    public static Language English => new("en");

    public override string ToString() => Value;
}
