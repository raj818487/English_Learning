using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Services;

public sealed class TopicService(IRepository<Topic> topicRepository, IUnitOfWork unitOfWork) : ITopicService
{
    public async Task<Topic> CreateAsync(Topic topic, CancellationToken cancellationToken = default)
    {
        var created = await topicRepository.AddAsync(topic, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return created;
    }

    public Task<IReadOnlyList<Topic>> ListAsync(CancellationToken cancellationToken = default) =>
        topicRepository.ListAsync(cancellationToken);
}
