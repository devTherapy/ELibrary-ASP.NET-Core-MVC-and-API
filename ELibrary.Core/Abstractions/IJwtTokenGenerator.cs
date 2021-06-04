using Microsoft.Extensions.Configuration;

namespace ELibrary.Core.Abstractions
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(string username, string userId, string email, IConfiguration config, string[] roles);
    }
}
