using EnglishLearning.Application.DTOs.TopicDTOs;
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Application.Mappings;

public static class MappingProfiles
{
    public static TopicDto ToDto(this Topic topic) => new()
    {
        Id = topic.Id,
        Name = topic.Name,
        Category = topic.Category,
        Difficulty = topic.Difficulty
    };
}
