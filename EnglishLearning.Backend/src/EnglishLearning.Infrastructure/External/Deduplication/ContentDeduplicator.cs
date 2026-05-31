using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.External.Deduplication;

public sealed class ContentDeduplicator : IContentDeduplicator
{
    private const int MaxAttempts = 3;
    private const double SimilarityThreshold = 0.85;

    public async Task<bool> IsDuplicateAsync(Content candidate, IEnumerable<Content> existing, CancellationToken cancellationToken = default)
    {
        for (var attempt = 0; attempt < MaxAttempts; attempt++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (HasExactDuplicate(candidate, existing) ||
                HasSemanticDuplicate(candidate, existing) ||
                HasMetadataDuplicate(candidate, existing))
            {
                return true;
            }

            await Task.Yield();
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
