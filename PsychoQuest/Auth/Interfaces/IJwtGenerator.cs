using System.Security.Claims;
using Entities.Models;

namespace Auth.Interfaces;

public interface IJwtGenerator
{
    string CreateToken(User user);
    
    string CreateRefreshToken();
    
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}