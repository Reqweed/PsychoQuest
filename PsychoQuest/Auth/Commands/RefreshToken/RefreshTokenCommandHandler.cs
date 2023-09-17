using System.Security.Claims;
using Auth.Interfaces;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand,AuthenticatedResponse>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<User> _userManager;

    public RefreshTokenCommandHandler(IJwtGenerator jwtGenerator, UserManager<User> userManager)
    {
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
    }

    public async Task<AuthenticatedResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = _jwtGenerator.GetPrincipalFromExpiredToken(request.Token);

        var userId = Convert.ToInt64(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        if(userId == 0) 
            throw new Exception();//fix exception

        var user = await _userManager.FindByIdAsync(userId.ToString()) 
                   ?? throw new Exception();//fix exception;

        if (user.RefreshToken != request.RefreshToken )
            throw new Exception();//fix exception
        
        var newToken = _jwtGenerator.CreateToken(user);
        
        var newRefreshToken = _jwtGenerator.CreateRefreshToken();
        user.RefreshToken = newRefreshToken;
        
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        await _userManager.UpdateAsync(user);
        
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