namespace EnglishLearning.Infrastructure.External.Deduplication;

public static class SimilarityCalculator
{
    public static double Calculate(string left, string right)
    {
        if (string.Equals(left, right, StringComparison.OrdinalIgnoreCase))
        {
            return 1d;
        }

        var leftWords = left.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToHashSet();
        var rightWords = right.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToHashSet();

        if (leftWords.Count == 0 && rightWords.Count == 0)
        {
            return 1d;
        }

        var intersection = leftWords.Intersect(rightWords).Count();
        var union = leftWords.Union(rightWords).Count();
        return union == 0 ? 0d : (double)intersection / union;
    }
}
