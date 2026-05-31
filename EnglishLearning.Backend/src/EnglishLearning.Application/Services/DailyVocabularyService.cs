using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Services;

public sealed class DailyVocabularyService(
    IRepository<DailyVocabularyWord> dailyVocabularyRepository,
    IUnitOfWork unitOfWork) : IDailyVocabularyService
{
    private static readonly string[] WordBank =
    [
        "abundant", "accomplish", "adapt", "adequate", "analyze", "approach", "arrange", "assist", "assume", "attempt",
        "balance", "benefit", "brief", "capable", "capture", "clarify", "combine", "commit", "compare", "complete",
        "conclude", "conduct", "confirm", "connect", "consider", "consistent", "construct", "context", "contribute", "convert",
        "cooperate", "creative", "crucial", "curious", "define", "deliver", "demonstrate", "describe", "develop", "device",
        "discover", "discuss", "display", "distinct", "diverse", "dynamic", "effective", "efficient", "element", "emerge",
        "emphasize", "encourage", "enhance", "evaluate", "evidence", "examine", "example", "expand", "explain", "explore",
        "express", "feature", "focus", "format", "frequent", "generate", "goal", "highlight", "identify", "impact",
        "improve", "include", "indicate", "influence", "inform", "initial", "insight", "inspire", "intense", "interpret",
        "introduce", "investigate", "involve", "justify", "knowledge", "language", "logical", "maintain", "manage", "measure",
        "method", "modify", "motivate", "observe", "obtain", "organize", "outcome", "participate", "pattern", "perform",
        "perspective", "positive", "practice", "predict", "prepare", "present", "priority", "process", "produce", "progress",
        "promote", "provide", "quality", "realize", "reason", "recognize", "recommend", "reflect", "relevant", "reliable",
        "require", "resolve", "respond", "result", "review", "routine", "schedule", "section", "select", "significant",
        "similar", "solution", "specific", "strategy", "structure", "succeed", "suitable", "support", "sustain", "target",
        "technique", "theory", "thrive", "transform", "understand", "unique", "update", "useful", "value", "variety"
    ];

    public async Task<IReadOnlyList<DailyVocabularyWord>> GenerateDailyWordsAsync(DateOnly date, int count = 50, CancellationToken cancellationToken = default)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than zero.");
        }

        var allWords = await dailyVocabularyRepository.ListAsync(cancellationToken);
        var wordsForDate = allWords.Where(x => x.GeneratedOnDate == date).ToList();
        var missingCount = Math.Max(0, count - wordsForDate.Count);

        if (missingCount > 0)
        {
            var existingWordSet = allWords
                .Select(x => x.Word)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var availableWords = WordBank
                .Where(x => !existingWordSet.Contains(x))
                .OrderBy(_ => Random.Shared.Next())
                .Take(missingCount)
                .ToList();

            if (availableWords.Count < missingCount)
            {
                throw new InvalidOperationException("Not enough unique words left to generate.");
            }

            foreach (var word in availableWords)
            {
                var item = new DailyVocabularyWord
                {
                    Word = word,
                    GeneratedOnDate = date
                };
                await dailyVocabularyRepository.AddAsync(item, cancellationToken);
                wordsForDate.Add(item);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return wordsForDate
            .OrderBy(x => x.Word, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    public async Task<IReadOnlyList<DailyVocabularyWord>> GetByDateAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        var allWords = await dailyVocabularyRepository.ListAsync(cancellationToken);
        return allWords
            .Where(x => x.GeneratedOnDate == date)
            .OrderBy(x => x.Word, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}
