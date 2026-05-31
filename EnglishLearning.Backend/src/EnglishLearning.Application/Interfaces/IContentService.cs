using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Application.Interfaces;

public interface IContentService
{
    Task<Content> CreateVocabularyAsync(Vocabulary vocabulary, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Content>> SearchAsync(string query, CancellationToken cancellationToken = default);
}
