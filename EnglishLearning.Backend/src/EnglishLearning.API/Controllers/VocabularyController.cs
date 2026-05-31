using EnglishLearning.API.Models;
using EnglishLearning.Application.DTOs.ContentDTOs;
using EnglishLearning.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class VocabularyController(IDailyVocabularyService dailyVocabularyService) : ControllerBase
{
    [HttpPost("daily/generate")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<DailyVocabularyWordDto>>>> GenerateDailyWords(
        [FromQuery] int count = 50,
        [FromQuery] DateOnly? date = null,
        CancellationToken cancellationToken = default)
    {
        var targetDate = date ?? DateOnly.FromDateTime(DateTime.UtcNow);
        var generated = await dailyVocabularyService.GenerateDailyWordsAsync(targetDate, count, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<DailyVocabularyWordDto>>.Ok(
            generated.Select(Map).ToList(),
            "Daily words generated successfully"));
    }

    [HttpGet("daily")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<DailyVocabularyWordDto>>>> GetDailyWords(
        [FromQuery] DateOnly? date = null,
        CancellationToken cancellationToken = default)
    {
        var targetDate = date ?? DateOnly.FromDateTime(DateTime.UtcNow);
        var words = await dailyVocabularyService.GetByDateAsync(targetDate, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<DailyVocabularyWordDto>>.Ok(words.Select(Map).ToList()));
    }

    private static DailyVocabularyWordDto Map(Domain.Entities.DailyVocabularyWord item) =>
        new()
        {
            Id = item.Id,
            Word = item.Word,
            GeneratedOnDate = item.GeneratedOnDate
        };
}
