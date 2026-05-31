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
    public DbSet<DailyVocabularyWord> DailyVocabularyWords => Set<DailyVocabularyWord>();
    public DbSet<UserProgress> UserProgresses => Set<UserProgress>();
    public DbSet<ContentHash> ContentHashes => Set<ContentHash>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>()
            .HasDiscriminator(c => c.ContentType)
            .HasValue<Vocabulary>(Domain.Enums.ContentTypeEnum.Vocabulary)
            .HasValue<GrammarRule>(Domain.Enums.ContentTypeEnum.GrammarRule)
            .HasValue<Sentence>(Domain.Enums.ContentTypeEnum.Sentence)
            .HasValue<Exercise>(Domain.Enums.ContentTypeEnum.Exercise)
            .HasValue<InterviewQuestion>(Domain.Enums.ContentTypeEnum.InterviewQuestion)
            .HasValue<ConversationScenario>(Domain.Enums.ContentTypeEnum.ConversationScenario);
        modelBuilder.Entity<ContentHash>().HasIndex(x => x.Hash).IsUnique();
        modelBuilder.Entity<DailyVocabularyWord>().HasIndex(x => x.Word).IsUnique();
        modelBuilder.Entity<DailyVocabularyWord>().HasIndex(x => x.GeneratedOnDate);
        base.OnModelCreating(modelBuilder);
    }
}
