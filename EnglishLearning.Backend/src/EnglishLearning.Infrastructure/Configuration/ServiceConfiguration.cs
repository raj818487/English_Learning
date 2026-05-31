using EnglishLearning.Application.Interfaces;
using EnglishLearning.Application.Services;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.External.Deduplication;
using EnglishLearning.Infrastructure.Persistence.Data;
using EnglishLearning.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Infrastructure.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("EnglishLearningDb"));

        services.AddScoped<IRepository<Content>, BaseRepository<Content>>();
        services.AddScoped<IRepository<Topic>, BaseRepository<Topic>>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IContentDeduplicator, ContentDeduplicator>();

        services.AddScoped<IDeduplicationService, DeduplicationService>();
        services.AddScoped<IContentService, ContentService>();
        services.AddScoped<ITopicService, TopicService>();

        return services;
    }
}
