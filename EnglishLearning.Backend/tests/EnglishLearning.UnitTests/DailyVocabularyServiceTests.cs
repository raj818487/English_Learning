using EnglishLearning.Application.Services;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using Moq;

namespace EnglishLearning.UnitTests;

public sealed class DailyVocabularyServiceTests
{
    [Fact]
    public async Task GenerateDailyWordsAsync_CreatesRequestedUniqueWords()
    {
        var store = new List<DailyVocabularyWord>();
        var repository = new Mock<IRepository<DailyVocabularyWord>>();
        repository.Setup(r => r.ListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(store);
        repository.Setup(r => r.AddAsync(It.IsAny<DailyVocabularyWord>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DailyVocabularyWord entity, CancellationToken _) =>
            {
                store.Add(entity);
                return entity;
            });

        var unitOfWork = new Mock<IUnitOfWork>();
        var service = new DailyVocabularyService(repository.Object, unitOfWork.Object);
        var date = new DateOnly(2026, 5, 31);

        var result = await service.GenerateDailyWordsAsync(date, 50);

        Assert.Equal(50, result.Count);
        Assert.Equal(50, result.Select(x => x.Word).Distinct(StringComparer.OrdinalIgnoreCase).Count());
        unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GenerateDailyWordsAsync_DoesNotRegenerate_WhenDateAlreadyHasRequestedCount()
    {
        var date = new DateOnly(2026, 5, 31);
        var store = Enumerable.Range(1, 50)
            .Select(i => new DailyVocabularyWord
            {
                Word = $"word-{i}",
                GeneratedOnDate = date
            })
            .ToList();

        var repository = new Mock<IRepository<DailyVocabularyWord>>();
        repository.Setup(r => r.ListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(store);

        var unitOfWork = new Mock<IUnitOfWork>();
        var service = new DailyVocabularyService(repository.Object, unitOfWork.Object);

        var result = await service.GenerateDailyWordsAsync(date, 50);

        Assert.Equal(50, result.Count);
        repository.Verify(r => r.AddAsync(It.IsAny<DailyVocabularyWord>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
