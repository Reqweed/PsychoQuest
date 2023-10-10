using Entities.Exceptions.NotFoundException;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Application.CommandsQueries.Role.Commands.SetRole;

public class SetRoleCommandHandler : IRequestHandler<SetRoleCommand>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly UserManager<Entities.Models.User> _userManager;
    private readonly ILoggerManager _loggerManager;

    public SetRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager, UserManager<Entities.Models.User> userManager, ILoggerManager loggerManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _loggerManager = loggerManager;
    }
    
    public async Task Handle(SetRoleCommand request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Command:SetRoleCommand - Role with id:{request.RoleId} has begun");

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        
        if (user is null)
        {
            _loggerManager.LogWarn($"Command:SetRoleCommand - User with id:{request.UserId} doesn't exist");

            throw new UserNotFoundException(request.UserId);
        }

        var role = await _roleManager.FindByIdAsync(request.UserId.ToString());
        
        if (role is null)
        {
            _loggerManager.LogWarn($"Command:SetRoleCommand - Role with id:{request.RoleId} doesn't exist");

            throw new RoleNotFoundException(request.RoleId);
        }

        var pastRole = await _userManager.GetRolesAsync(user);
        
        if (pastRole is null)
        {
            _loggerManager.LogWarn($"Command:SetRoleCommand - User with id:{request.UserId} hadn't Past_Role doesn't exist");

            throw new RoleNotFoundException(user.Id);
        }

        await _userManager.RemoveFromRolesAsync(user, pastRole);

        var result = await _userManager.AddToRoleAsync(user, role.ToString());
        
        if (!result.Succeeded)
        {
            _loggerManager.LogWarn($"Command:SetRoleCommand - User with id:{request.UserId} set Role with id:{request.RoleId} wasn't add successfully");

            throw new Exception();
        } 
        
        _loggerManager.LogInfo($"Command:SetRoleCommand - Role with id:{request.RoleId} was finished");
    }
}