using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.External.Deduplication;

public sealed class ContentDeduplicator : IContentDeduplicator
{
    private const int MaxAttempts = 3;
    private const double SimilarityThreshold = 0.85;

    public async Task<bool> IsDuplicateAsync(Content candidate, IEnumerable<Content> existing, CancellationToken cancellationToken = default)
    {
        for (var attempt = 1; attempt <= MaxAttempts; attempt++)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (HasExactDuplicate(candidate, existing) ||
                    HasSemanticDuplicate(candidate, existing) ||
                    HasMetadataDuplicate(candidate, existing))
                {
                    return true;
                }

                return false;
            }
            catch when (attempt < MaxAttempts)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(50 * attempt), cancellationToken);
            }
        }

        return false;
    }

    private static bool HasExactDuplicate(Content candidate, IEnumerable<Content> existing)
    {
        var candidateHash = HashGenerator.GenerateSha256($"{candidate.Title}|{candidate.Text}");
        return existing.Any(item =>
            HashGenerator.GenerateSha256($"{item.Title}|{item.Text}") == candidateHash);
    }

    private static bool HasSemanticDuplicate(Content candidate, IEnumerable<Content> existing)
        => existing.Any(item => SimilarityCalculator.Calculate($"{candidate.Title} {candidate.Text}", $"{item.Title} {item.Text}") >= SimilarityThreshold);

    private static bool HasMetadataDuplicate(Content candidate, IEnumerable<Content> existing)
        => existing.Any(item => item.TopicId == candidate.TopicId &&
                                item.ContentType == candidate.ContentType &&
                                string.Equals(item.Title, candidate.Title, StringComparison.OrdinalIgnoreCase));
}
