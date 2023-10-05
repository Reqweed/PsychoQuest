using MediatR;

namespace Application.CommandsQueries.Role.Commands.CreateRole;

public class CreateRoleCommand : IRequest
{
    public string RoleName { get; set; } = string.Empty;
}