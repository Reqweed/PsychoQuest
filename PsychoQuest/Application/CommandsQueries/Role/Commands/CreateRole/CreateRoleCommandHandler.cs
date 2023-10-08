using Entities.Exceptions.BadRequestException;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandsQueries.Role.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public CreateRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole<long>(request.RoleName));
        
        if (!result.Succeeded) 
            throw new CreateRoleBadRequestException();
    }
}