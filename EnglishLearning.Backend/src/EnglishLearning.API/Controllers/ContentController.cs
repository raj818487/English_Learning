using EnglishLearning.API.Models;
using EnglishLearning.Application.DTOs.ContentDTOs;
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ContentController(IContentService contentService) : ControllerBase
{
    [HttpPost("vocabulary")]
    public async Task<ActionResult<ApiResponse<Content>>> CreateVocabulary([FromBody] VocabularyDto request, CancellationToken cancellationToken)
    {
        var vocabulary = new Vocabulary
        {
            TopicId = request.TopicId,
            Title = request.Title,
            Text = request.Text,
            Definition = request.Definition,
            Difficulty = request.Difficulty
        };

        var created = await contentService.CreateVocabularyAsync(vocabulary, cancellationToken);
        return Ok(ApiResponse<Content>.Ok(created));
    }

    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<Content>>>> Search([FromQuery] string query, CancellationToken cancellationToken)
    {
        var result = await contentService.SearchAsync(query, cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<Content>>.Ok(result));
    }
}
