using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Application.Interfaces;

public interface ITopicService
{
    Task<Topic> CreateAsync(Topic topic, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Topic>> ListAsync(CancellationToken cancellationToken = default);
}
