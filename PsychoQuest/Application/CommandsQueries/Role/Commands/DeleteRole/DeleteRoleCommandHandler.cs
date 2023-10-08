using Entities.Exceptions.NotFoundException;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandsQueries.Role.Commands.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public DeleteRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString()) 
                   ?? throw new RoleNotFoundException(request.RoleId);
        
        var result = await _roleManager.DeleteAsync(role);
        
        if (!result.Succeeded) 
            throw new Exception("Role wasn't deleted");
    }
}