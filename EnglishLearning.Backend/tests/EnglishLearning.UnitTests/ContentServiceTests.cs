using EnglishLearning.Application.Interfaces;
using EnglishLearning.Application.Services;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using Moq;

namespace EnglishLearning.UnitTests;

public sealed class ContentServiceTests
{
    [Fact]
    public async Task CreateVocabularyAsync_PersistsVocabulary()
    {
        var repository = new Mock<IRepository<Content>>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var deduplicationService = new Mock<IDeduplicationService>();

        repository.Setup(r => r.AddAsync(It.IsAny<Content>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Content content, CancellationToken _) => content);

        var service = new ContentService(repository.Object, deduplicationService.Object, unitOfWork.Object);
        var vocabulary = new Vocabulary { Title = "Word", Text = "Definition", TopicId = Guid.NewGuid() };

        var created = await service.CreateVocabularyAsync(vocabulary);

        Assert.Equal("Word", created.Title);
        unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
