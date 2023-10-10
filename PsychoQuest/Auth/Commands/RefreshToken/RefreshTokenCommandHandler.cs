using System.Security.Claims;
using Auth.Interfaces;
using Entities.Exceptions.BadRequestException;
using Entities.Exceptions.NotFoundException;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand,AuthenticatedResponse>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<User> _userManager;
    private readonly ILoggerManager _loggerManager;

    public RefreshTokenCommandHandler(IJwtGenerator jwtGenerator, UserManager<User> userManager, ILoggerManager loggerManager)
    {
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
        _loggerManager = loggerManager;
    }

    public async Task<AuthenticatedResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Command:RefreshTokenCommand - User with id:{request.UserId} has begun");
        
        var principal = _jwtGenerator.GetPrincipalFromExpiredToken(request.Token);

        var userId = Convert.ToInt64(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        if (userId == 0)
        {
            _loggerManager.LogWarn($"Command:RefreshTokenCommand - User with id:{request.UserId} has bad token");

            throw new TokenBadRequestException();
        } 

        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user is null)
        {
            _loggerManager.LogWarn($"Command:RefreshTokenCommand - User with id:{request.UserId} doesn't exist");

            throw new UserNotFoundException(userId);
        }

        if (user.RefreshToken != request.RefreshToken)
        {
            _loggerManager.LogWarn($"Command:RefreshTokenCommand - User with id:{request.UserId} has bad refresh token");

            throw new RefreshTokenBadRequestException();
        }
        
        var newToken = _jwtGenerator.CreateToken(user);
        
        var newRefreshToken = _jwtGenerator.CreateRefreshToken();
        user.RefreshToken = newRefreshToken;
        
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        await _userManager.UpdateAsync(user);
        
        _loggerManager.LogInfo($"Command:RefreshTokenCommand - User with id:{request.UserId} was finished");
        
        return new AuthenticatedResponse()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Token = newToken,
            RefreshToken = newRefreshToken
        };
    }
}