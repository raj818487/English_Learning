using System.Security.Cryptography;
using System.Text;

namespace EnglishLearning.Infrastructure.External.Deduplication;

public static class HashGenerator
{
    public static string GenerateSha256(string content)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(content.Trim().ToLowerInvariant()));
        return Convert.ToHexString(bytes);
    }
}
