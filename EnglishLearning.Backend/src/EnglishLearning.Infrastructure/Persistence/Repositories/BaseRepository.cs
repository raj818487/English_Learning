using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Persistence.Repositories;

public class BaseRepository<T>(ApplicationDbContext dbContext) : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext DbContext = dbContext;

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<T>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        DbContext.Set<T>().FindAsync([id], cancellationToken).AsTask();

    public async Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken = default) =>
        await DbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().Update(entity);
        return Task.CompletedTask;
    }
}
