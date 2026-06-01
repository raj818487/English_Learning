using EnglishLearning.API.Models;
using EnglishLearning.Application.DTOs.InterviewDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnglishLearning.Application.Interfaces;

namespace EnglishLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class InterviewQuestionsController(IInterviewService interviewService) : ControllerBase
{
	[HttpGet]
	[AllowAnonymous]
	public async Task<ActionResult<ApiResponse<IReadOnlyList<InterviewQuestionDto>>>> List(CancellationToken cancellationToken)
	{
		var items = await interviewService.ListAsync(cancellationToken);
		var dtos = items.Select(i => new InterviewQuestionDto
		{
			TopicId = i.TopicId,
			Title = i.Title,
			Text = i.Text,
			SuggestedAnswer = i.SuggestedAnswer,
			Difficulty = i.Difficulty
		}).ToList();

		return Ok(ApiResponse<IReadOnlyList<InterviewQuestionDto>>.Ok(dtos));
	}

	[HttpPost]
	public async Task<ActionResult<ApiResponse<InterviewQuestionDto>>> Create([FromBody] InterviewQuestionDto request, CancellationToken cancellationToken)
	{
		var entity = new EnglishLearning.Domain.Entities.InterviewQuestion
		{
			TopicId = request.TopicId,
			Title = request.Title,
			Text = request.Text,
			SuggestedAnswer = request.SuggestedAnswer,
			Difficulty = request.Difficulty
		};

		var created = await interviewService.CreateAsync(entity, cancellationToken);
		var dto = new InterviewQuestionDto
		{
			TopicId = created.TopicId,
			Title = created.Title,
			Text = created.Text,
			SuggestedAnswer = created.SuggestedAnswer,
			Difficulty = created.Difficulty
		};

		return Ok(ApiResponse<InterviewQuestionDto>.Ok(dto));
	}

	[HttpPut("{id:guid}")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<ApiResponse<InterviewQuestionDto>>> Update(Guid id, [FromBody] InterviewQuestionDto request, CancellationToken cancellationToken)
	{
		var existing = await interviewService.GetByIdAsync(id, cancellationToken);
		if (existing == null) return NotFound(ApiResponse<InterviewQuestionDto>.Fail("Not found"));

		existing.TopicId = request.TopicId;
		existing.Title = request.Title;
		existing.Text = request.Text;
		existing.SuggestedAnswer = request.SuggestedAnswer;
		existing.Difficulty = request.Difficulty;

		await interviewService.UpdateAsync(existing, cancellationToken);

		var dto = new InterviewQuestionDto
		{
			TopicId = existing.TopicId,
			Title = existing.Title,
			Text = existing.Text,
			SuggestedAnswer = existing.SuggestedAnswer,
			Difficulty = existing.Difficulty
		};

		return Ok(ApiResponse<InterviewQuestionDto>.Ok(dto));
	}

	[HttpDelete("{id:guid}")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id, CancellationToken cancellationToken)
	{
		var existing = await interviewService.GetByIdAsync(id, cancellationToken);
		if (existing == null) return NotFound(ApiResponse<object>.Fail("Not found"));

		await interviewService.DeleteAsync(existing, cancellationToken);
		return Ok(ApiResponse<object>.Ok(new { }));
	}
}
