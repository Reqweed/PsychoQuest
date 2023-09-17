using MediatR;

namespace Auth.Commands.UpdateUser;

public class UpdateUserCommand : IRequest
{
    public long UserId { get; set; }
    
    public string? NewUserName { get; set; }
    
    public string? NewEmail { get; set; }

    public string OldPassword { get; set; }
    
    public string? NewPassword { get; set; }
}