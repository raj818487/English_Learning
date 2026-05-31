using EnglishLearning.Domain.Entities;
using EnglishLearning.Infrastructure.Persistence.Data;

namespace EnglishLearning.Infrastructure.Persistence.Repositories;

public sealed class VocabularyRepository(ApplicationDbContext dbContext) : BaseRepository<Vocabulary>(dbContext);
