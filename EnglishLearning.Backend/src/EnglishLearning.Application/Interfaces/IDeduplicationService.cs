using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Application.Interfaces;

public interface IDeduplicationService
{
    Task EnsureNoDuplicateAsync(Content candidate, CancellationToken cancellationToken = default);
}
