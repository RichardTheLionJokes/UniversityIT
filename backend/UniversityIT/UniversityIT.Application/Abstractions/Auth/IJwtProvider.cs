using System.Security.Claims;
using UniversityIT.Core.Models.Auth;

namespace UniversityIT.Application.Abstractions.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
        Guid GetUserIdByToken(string token);
    }
}