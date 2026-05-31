using Microsoft.AspNetCore.Mvc;
using EnglishLearning.Application.DTOs.TopicDTOs;
using EnglishLearning.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishLearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly ILogger<TopicsController> _logger;

        public TopicsController(ITopicService topicService, ILogger<TopicsController> logger)
        {
            _topicService = topicService;
            _logger = logger;
        }

        /// <summary>
        /// Get all topics
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicResponseDto>>> GetAllTopics()
        {
            try
            {
                var topics = await _topicService.GetAllTopicsAsync();
                return Ok(topics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting topics");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get topic by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicResponseDto>> GetTopicById(Guid id)
        {
            try
            {
                var topic = await _topicService.GetTopicByIdAsync(id);
                if (topic == null)
                    return NotFound(new { message = "Topic not found" });

                return Ok(topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting topic");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Create new topic
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TopicResponseDto>> CreateTopic([FromBody] CreateTopicDto createTopicDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var topic = await _topicService.CreateTopicAsync(createTopicDto);
                return CreatedAtAction(nameof(GetTopicById), new { id = topic.Id }, topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating topic");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Update topic
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTopic(Guid id, [FromBody] UpdateTopicDto updateTopicDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var success = await _topicService.UpdateTopicAsync(id, updateTopicDto);
                if (!success)
                    return NotFound(new { message = "Topic not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating topic");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Delete topic
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(Guid id)
        {
            try
            {
                var success = await _topicService.DeleteTopicAsync(id);
                if (!success)
                    return NotFound(new { message = "Topic not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting topic");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get topics by category
        /// </summary>
        [HttpGet("by-category/{category}")]
        public async Task<ActionResult<IEnumerable<TopicResponseDto>>> GetTopicsByCategory(string category)
        {
            try
            {
                var topics = await _topicService.GetTopicsByCategoryAsync(category);
                return Ok(topics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting topics by category");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get topics by difficulty
        /// </summary>
        [HttpGet("by-difficulty/{difficulty}")]
        public async Task<ActionResult<IEnumerable<TopicResponseDto>>> GetTopicsByDifficulty(int difficulty)
        {
            try
            {
                var topics = await _topicService.GetTopicsByDifficultyAsync(difficulty);
                return Ok(topics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting topics by difficulty");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}
