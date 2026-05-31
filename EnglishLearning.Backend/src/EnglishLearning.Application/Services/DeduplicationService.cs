using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Exceptions;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Services;

public sealed class DeduplicationService(
    IRepository<Content> contentRepository,
    IContentDeduplicator deduplicator) : IDeduplicationService
{
    public async Task EnsureNoDuplicateAsync(Content candidate, CancellationToken cancellationToken = default)
    {
        var existing = await contentRepository.ListAsync(cancellationToken);
        var isDuplicate = await deduplicator.IsDuplicateAsync(candidate, existing, cancellationToken);
        if (isDuplicate)
        {
            throw new DuplicateContentException("Duplicate content detected.");
        }
    }
}
