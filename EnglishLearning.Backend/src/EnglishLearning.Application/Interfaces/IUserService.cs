using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Application.Interfaces;

public interface IUserService
{
    Task<User> CreateAsync(string email, string password, string role = "User", CancellationToken cancellationToken = default);
    Task<User?> ValidateCredentialsAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
