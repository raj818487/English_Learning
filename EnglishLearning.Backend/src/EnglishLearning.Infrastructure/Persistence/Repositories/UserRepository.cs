using EnglishLearning.Domain.Entities;
using EnglishLearning.Infrastructure.Persistence.Data;

namespace EnglishLearning.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext) : BaseRepository<User>(dbContext);
