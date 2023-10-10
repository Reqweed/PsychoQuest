using Entities.Models;
using MediatR;

namespace Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthenticatedResponse>
{
    public long UserId { get; set; }
    public string Token { get; set; }
    
    public string RefreshToken { get; set; }
}