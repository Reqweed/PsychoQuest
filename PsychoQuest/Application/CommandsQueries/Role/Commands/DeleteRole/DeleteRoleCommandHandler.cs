using Entities.Exceptions.NotFoundException;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Application.CommandsQueries.Role.Commands.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly ILoggerManager _loggerManager;

    public DeleteRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager, ILoggerManager loggerManager)
    {
        _roleManager = roleManager;
        _loggerManager = loggerManager;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Command:DeleteRoleCommand - Role with id:{request.RoleId} has begun");
        
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is null)
        {
            _loggerManager.LogWarn($"Command:DeleteRoleCommand - Role with name:{request.RoleId} doesn't exist");

            throw new RoleNotFoundException(request.RoleId);
        }

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            _loggerManager.LogWarn($"Command:DeleteRoleCommand - Role with name:{request.RoleId} doesn't succeeded");

            throw new Exception("Role wasn't deleted");
        }
        
        _loggerManager.LogInfo($"Command:DeleteRoleCommand - Role with id:{request.RoleId} was finished");
    }
}