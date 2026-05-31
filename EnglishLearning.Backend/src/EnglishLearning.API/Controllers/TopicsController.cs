using EnglishLearning.API.Models;
using EnglishLearning.Application.DTOs.TopicDTOs;
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Application.Mappings;
using EnglishLearning.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TopicsController(ITopicService topicService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ApiResponse<TopicDto>>> Create([FromBody] TopicDto request, CancellationToken cancellationToken)
    {
        var created = await topicService.CreateAsync(new Topic
        {
            Name = request.Name,
            Category = request.Category,
            Difficulty = request.Difficulty
        }, cancellationToken);

        return Ok(ApiResponse<TopicDto>.Ok(created.ToDto()));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<TopicDto>>>> List(CancellationToken cancellationToken)
    {
        var topics = await topicService.ListAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<TopicDto>>.Ok(topics.Select(x => x.ToDto()).ToList()));
    }
}
