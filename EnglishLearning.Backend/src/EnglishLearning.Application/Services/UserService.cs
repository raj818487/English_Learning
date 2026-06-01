using System.Security.Cryptography;
using System.Text;
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Services;

public sealed class UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork) : IUserService
{
    public async Task<User> CreateAsync(string email, string password, string role = "User", CancellationToken cancellationToken = default)
    {
        var hash = HashPassword(password);
        var user = new User { Email = email, PasswordHash = hash, Role = role };
        var created = await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return created;
    }

    public async Task<User?> ValidateCredentialsAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var all = await userRepository.ListAsync(cancellationToken);
        var user = all.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (user == null) return null;
        return VerifyPassword(password, user.PasswordHash) ? user : null;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var all = await userRepository.ListAsync(cancellationToken);
        return all.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    // PBKDF2
    private static string HashPassword(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[16];
        rng.GetBytes(salt);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(32);
        var result = new byte[49]; // 16 salt + 32 hash + 1 version
        result[0] = 0x01; // version
        Buffer.BlockCopy(salt, 0, result, 1, 16);
        Buffer.BlockCopy(hash, 0, result, 17, 32);
        return Convert.ToBase64String(result);
    }

    private static bool VerifyPassword(string password, string stored)
    {
        try
        {
            var bytes = Convert.FromBase64String(stored);
            if (bytes.Length != 49 || bytes[0] != 0x01) return false;
            var salt = new byte[16];
            Buffer.BlockCopy(bytes, 1, salt, 0, 16);
            var hash = new byte[32];
            Buffer.BlockCopy(bytes, 17, hash, 0, 32);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            var computed = pbkdf2.GetBytes(32);
            return CryptographicOperations.FixedTimeEquals(computed, hash);
        }
        catch
        {
            return false;
        }
    }
}
