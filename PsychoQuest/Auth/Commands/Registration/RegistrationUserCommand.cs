using Entities.Enums;
using Entities.Models;
using MediatR;

namespace Auth.Commands.Registration;

public class RegistrationUserCommand : IRequest<AuthenticatedResponse>
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    
    public Gender Gender { get; set; }
    
    public DateTime BirthDate { get; set; }
}