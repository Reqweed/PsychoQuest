using Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models;

public class User : IdentityUser<long>
{
    public Gender Gender { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }
}