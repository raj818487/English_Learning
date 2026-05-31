using EnglishLearning.Domain.Entities;
using EnglishLearning.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Persistence.Repositories;

public sealed class ContentRepository(ApplicationDbContext dbContext) : BaseRepository<Content>(dbContext)
{
    public async Task<IReadOnlyList<Content>> SearchAsync(string query, CancellationToken cancellationToken = default) =>
        await DbContext.Contents.Where(c => c.Title.Contains(query) || c.Text.Contains(query)).ToListAsync(cancellationToken);
}
