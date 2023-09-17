using System.Security.Claims;
using Auth.Interfaces;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<User> _userManager;

    public UpdateUserCommandHandler(IJwtGenerator jwtGenerator, UserManager<User> userManager)
    {
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString()) 
                   ?? throw new Exception();//fix exception
        
        if (!await _userManager.CheckPasswordAsync(user, request.OldPassword))
            throw new Exception("Invalid password");//fix exception

        if (request.NewUserName is not null) 
            user.UserName = request.NewUserName;
        if (request.NewEmail is not null) 
            user.Email = request.NewEmail;
        if (request.NewPassword is not null) 
            await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        
        await _userManager.UpdateAsync(user);
    }
}