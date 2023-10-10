using Entities.Exceptions.BadRequestException;
using Entities.Exceptions.NotFoundException;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Auth.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILoggerManager _loggerManager;

    public UpdateUserCommandHandler(UserManager<User> userManager, ILoggerManager loggerManager)
    {
        _userManager = userManager;
        _loggerManager = loggerManager;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Command:UpdateUserCommand - User with id:{request.UserId} has begun");

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        
        if(user is null)
        {
            _loggerManager.LogWarn($"Command:UpdateUserCommand - User with id:{request.UserId} doesn't exist");

            throw new UserNotFoundException(request.UserId);
        }

        if (!await _userManager.CheckPasswordAsync(user, request.OldPassword))
        {
            _loggerManager.LogWarn($"Command:UpdateUserCommand - User with id:{request.UserId} with invalid password");

            throw new DataUserBadRequestException(nameof(User));
        }

        if (request.NewUserName is not null) 
            user.UserName = request.NewUserName;
        if (request.NewEmail is not null) 
            user.Email = request.NewEmail;
        if (request.NewPassword is not null) 
            await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        
        await _userManager.UpdateAsync(user);
        
        _loggerManager.LogInfo($"Command:UpdateUserCommand - User with id:{request.UserId} was finished");
    }
}