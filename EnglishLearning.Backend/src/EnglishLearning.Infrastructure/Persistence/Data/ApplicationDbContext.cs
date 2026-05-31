using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Persistence.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Content> Contents => Set<Content>();
    public DbSet<Vocabulary> Vocabularies => Set<Vocabulary>();
    public DbSet<GrammarRule> GrammarRules => Set<GrammarRule>();
    public DbSet<Sentence> Sentences => Set<Sentence>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<InterviewQuestion> InterviewQuestions => Set<InterviewQuestion>();
    public DbSet<ConversationScenario> ConversationScenarios => Set<ConversationScenario>();
    public DbSet<UserProgress> UserProgresses => Set<UserProgress>();
    public DbSet<ContentHash> ContentHashes => Set<ContentHash>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>().HasDiscriminator(c => c.ContentType);
        modelBuilder.Entity<ContentHash>().HasIndex(x => x.Hash).IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}
