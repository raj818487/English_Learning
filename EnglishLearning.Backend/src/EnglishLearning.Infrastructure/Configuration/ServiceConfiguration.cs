using EnglishLearning.Application.Interfaces;
using EnglishLearning.Application.Services;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.External.Deduplication;
using EnglishLearning.Infrastructure.Persistence.Data;
using EnglishLearning.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Infrastructure.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=english_learning;Username=postgres;Password=postgres";

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, npgsql =>
                npgsql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IRepository<Content>, BaseRepository<Content>>();
            services.AddScoped<IRepository<Topic>, BaseRepository<Topic>>();
            services.AddScoped<IRepository<User>, UserRepository>();
        services.AddScoped<IRepository<InterviewQuestion>, InterviewQuestionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IContentDeduplicator, ContentDeduplicator>();

        services.AddScoped<IDeduplicationService, DeduplicationService>();
        services.AddScoped<IContentService, ContentService>();
        services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IInterviewService, InterviewService>();
            services.AddScoped<IUserService, UserService>();

        return services;
    }
}
