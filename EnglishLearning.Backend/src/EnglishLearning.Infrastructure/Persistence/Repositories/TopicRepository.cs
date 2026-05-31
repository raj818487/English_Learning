using EnglishLearning.Domain.Entities;
using EnglishLearning.Infrastructure.Persistence.Data;

namespace EnglishLearning.Infrastructure.Persistence.Repositories;

public sealed class TopicRepository(ApplicationDbContext dbContext) : BaseRepository<Topic>(dbContext);
