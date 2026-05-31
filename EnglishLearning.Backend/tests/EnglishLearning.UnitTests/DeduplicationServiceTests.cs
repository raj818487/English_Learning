using EnglishLearning.Application.Services;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Exceptions;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.External.Deduplication;
using Moq;

namespace EnglishLearning.UnitTests;

public sealed class DeduplicationServiceTests
{
    [Fact]
    public async Task EnsureNoDuplicateAsync_Throws_WhenDuplicateDetected()
    {
        var existing = new List<Content>
        {
            new Vocabulary { Title = "Greeting", Text = "Hello world", TopicId = Guid.NewGuid() }
        };

        var repository = new Mock<IRepository<Content>>();
        repository.Setup(r => r.ListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(existing);

        var service = new DeduplicationService(repository.Object, new ContentDeduplicator());
        var candidate = new Vocabulary { Title = "Greeting", Text = "Hello world", TopicId = existing[0].TopicId };

        await Assert.ThrowsAsync<DuplicateContentException>(() => service.EnsureNoDuplicateAsync(candidate));
    }
}
