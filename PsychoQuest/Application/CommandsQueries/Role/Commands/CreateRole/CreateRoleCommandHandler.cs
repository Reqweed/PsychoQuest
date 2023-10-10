using Entities.Exceptions.BadRequestException;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Application.CommandsQueries.Role.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly ILoggerManager _loggerManager;
    public CreateRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager, ILoggerManager loggerManager)
    {
        _roleManager = roleManager;
        _loggerManager = loggerManager;
    }

    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Command:CreateRoleCommand - Role with name:{request.RoleName} has begun");

        var result = await _roleManager.CreateAsync(new IdentityRole<long>(request.RoleName));

        if (!result.Succeeded)
        {
            _loggerManager.LogWarn($"Command:CreateRoleCommand - Role with name:{request.RoleName} doesn't succeeded");
            
            throw new CreateRoleBadRequestException();
        } 
            
        
        _loggerManager.LogInfo($"Command:CreateRoleCommand - User with name:{request.RoleName} was finished");
    }
}