using MediatR;

namespace Application.CommandsQueries.Role.Commands.DeleteRole;

public class DeleteRoleCommand : IRequest
{
    public long RoleId { get; set; }
}