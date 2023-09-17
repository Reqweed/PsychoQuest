using Entities.Enums;

namespace Entities.Models;

public class AuthenticatedResponse
{
    public long UserId { get; set; }
    
    public string? UserName { get; set; } = string.Empty;
    public string? Email { get; set; }

    public Gender Gender { get; set; }
    
    public DateTime BirthDate { get; set; }

    public string Token { get; set; } = string.Empty;
    
    public string RefreshToken { get; set; } = string.Empty;
}