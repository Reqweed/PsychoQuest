using MediatR;

namespace Application.CommandsQueries.User.Commands.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public long UserId { get; set; }
}