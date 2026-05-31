using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Application.Interfaces;

public interface IDailyVocabularyService
{
    Task<IReadOnlyList<DailyVocabularyWord>> GenerateDailyWordsAsync(DateOnly date, int count = 50, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DailyVocabularyWord>> GetByDateAsync(DateOnly date, CancellationToken cancellationToken = default);
}
