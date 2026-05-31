using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Infrastructure.Persistence.Data;

public static class SeedData
{
    public static async Task InitializeAsync(ApplicationDbContext dbContext)
    {
        if (dbContext.Topics.Any())
        {
            return;
        }

        dbContext.Topics.Add(new Topic
        {
            Name = "Interview Basics",
            Category = TopicCategoryEnum.Interview,
            Difficulty = DifficultyEnum.Intermediate
        });

        await dbContext.SaveChangesAsync();
    }
}
