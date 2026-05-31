using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence.Data;

namespace EnglishLearning.Infrastructure.Persistence.Repositories;

public sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
