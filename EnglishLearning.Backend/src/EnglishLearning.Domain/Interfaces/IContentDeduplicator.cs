using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Domain.Interfaces;

public interface IContentDeduplicator
{
    Task<bool> IsDuplicateAsync(Content candidate, IEnumerable<Content> existing, CancellationToken cancellationToken = default);
}
