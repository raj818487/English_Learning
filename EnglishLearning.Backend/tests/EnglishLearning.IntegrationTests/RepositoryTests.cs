using EnglishLearning.Domain.Entities;
using EnglishLearning.Infrastructure.Persistence.Data;
using EnglishLearning.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.IntegrationTests;

public sealed class RepositoryTests
{
    [Fact]
    public async Task BaseRepository_AddsEntity()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var dbContext = new ApplicationDbContext(options);
        var repository = new BaseRepository<Topic>(dbContext);

        await repository.AddAsync(new Topic { Name = "Grammar" });
        await dbContext.SaveChangesAsync();

        Assert.Single(await repository.ListAsync());
    }
}
