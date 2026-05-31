using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Exceptions;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Services;

public sealed class ContentService(
    IRepository<Content> contentRepository,
    IDeduplicationService deduplicationService,
    IUnitOfWork unitOfWork) : IContentService
{
    public async Task<Content> CreateVocabularyAsync(Vocabulary vocabulary, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(vocabulary.Title) || string.IsNullOrWhiteSpace(vocabulary.Text))
        {
            throw new ContentValidationException("Title and text are required.");
        }

        await deduplicationService.EnsureNoDuplicateAsync(vocabulary, cancellationToken);
        var created = await contentRepository.AddAsync(vocabulary, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return created;
    }

    public async Task<IReadOnlyList<Content>> SearchAsync(string query, CancellationToken cancellationToken = default)
    {
        var all = await contentRepository.ListAsync(cancellationToken);
        return all.Where(c => c.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                              c.Text.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
