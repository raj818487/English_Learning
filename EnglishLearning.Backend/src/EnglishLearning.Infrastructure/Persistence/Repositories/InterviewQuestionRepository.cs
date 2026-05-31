using EnglishLearning.Domain.Entities;
using EnglishLearning.Infrastructure.Persistence.Data;

namespace EnglishLearning.Infrastructure.Persistence.Repositories;

public sealed class InterviewQuestionRepository(ApplicationDbContext dbContext) : BaseRepository<InterviewQuestion>(dbContext);
