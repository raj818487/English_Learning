using Microsoft.AspNetCore.Mvc;
using EnglishLearning.Application.DTOs.InterviewDTOs;
using EnglishLearning.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishLearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewQuestionsController : ControllerBase
    {
        private readonly IInterviewService _interviewService;
        private readonly ILogger<InterviewQuestionsController> _logger;

        public InterviewQuestionsController(IInterviewService interviewService, ILogger<InterviewQuestionsController> logger)
        {
            _interviewService = interviewService;
            _logger = logger;
        }

        /// <summary>
        /// Get all interview questions
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterviewQuestionResponseDto>>> GetAllQuestions()
        {
            try
            {
                var questions = await _interviewService.GetAllQuestionsAsync();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting interview questions");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get interview question by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewQuestionResponseDto>> GetQuestionById(Guid id)
        {
            try
            {
                var question = await _interviewService.GetQuestionByIdAsync(id);
                if (question == null)
                    return NotFound(new { message = "Question not found" });

                return Ok(question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting interview question");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Create new interview question
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<InterviewQuestionResponseDto>> CreateQuestion([FromBody] CreateInterviewQuestionDto createQuestionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var question = await _interviewService.CreateQuestionAsync(createQuestionDto);
                return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating interview question");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Update interview question
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, [FromBody] UpdateInterviewQuestionDto updateQuestionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var success = await _interviewService.UpdateQuestionAsync(id, updateQuestionDto);
                if (!success)
                    return NotFound(new { message = "Question not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating interview question");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Delete interview question
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            try
            {
                var success = await _interviewService.DeleteQuestionAsync(id);
                if (!success)
                    return NotFound(new { message = "Question not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting interview question");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get questions by category
        /// </summary>
        [HttpGet("by-category/{category}")]
        public async Task<ActionResult<IEnumerable<InterviewQuestionResponseDto>>> GetQuestionsByCategory(string category)
        {
            try
            {
                var questions = await _interviewService.GetQuestionsByCategoryAsync(category);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting questions by category");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get questions by company
        /// </summary>
        [HttpGet("by-company/{company}")]
        public async Task<ActionResult<IEnumerable<InterviewQuestionResponseDto>>> GetQuestionsByCompany(string company)
        {
            try
            {
                var questions = await _interviewService.GetQuestionsByCompanyAsync(company);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting questions by company");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Submit practice answer and get feedback
        /// </summary>
        [HttpPost("{id}/practice")]
        public async Task<ActionResult<InterviewFeedbackDto>> PracticeQuestion(Guid id, [FromBody] InterviewPracticeDto practiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var feedback = await _interviewService.ProvideFeedbackAsync(id, practiceDto.UserAnswer);
                if (feedback == null)
                    return NotFound(new { message = "Question not found" });

                return Ok(feedback);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error providing feedback");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}
