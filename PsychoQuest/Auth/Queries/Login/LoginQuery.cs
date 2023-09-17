using Entities.Models;
using MediatR;

namespace Auth.Queries.Login;

public class LoginQuery : IRequest<AuthenticatedResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}