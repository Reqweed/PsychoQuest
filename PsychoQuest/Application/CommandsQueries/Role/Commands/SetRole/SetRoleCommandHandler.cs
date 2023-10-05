using Entities.Exceptions.NotFoundException;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandsQueries.Role.Commands.SetRole;

public class SetRoleCommandHandler : IRequestHandler<SetRoleCommand>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly UserManager<Entities.Models.User> _userManager;

    public SetRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager, UserManager<Entities.Models.User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    
    public async Task Handle(SetRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString()) 
                   ?? throw new UserNotFoundException(request.UserId);

        var role = await _roleManager.FindByIdAsync(request.UserId.ToString()) 
                   ?? throw new Exception();//fix

        var pastRole = await _userManager.GetRolesAsync(user) 
                       ?? throw new Exception();//fix

        await _userManager.RemoveFromRolesAsync(user, pastRole);

        var result = await _userManager.AddToRoleAsync(user, role.ToString());
       
        if (!result.Succeeded) 
            throw new Exception();
    }
}