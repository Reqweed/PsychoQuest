using Auth.Interfaces;
using Entities.Exceptions.BadRequestException;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Auth.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery,AuthenticatedResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly ILoggerManager _loggerManager;

    public LoginQueryHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator, ILoggerManager loggerManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _loggerManager = loggerManager;
    }

    public async Task<AuthenticatedResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:LoginQuery - User:{request.Email} has begun");
        
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user is null)
        {
            _loggerManager.LogWarn($"Query:LoginQuery - User:{request.Email} doesn't exist");

             throw new DataUserBadRequestException(nameof(request.Email));
        }
        
        var result = await _signInManager
            .CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            _loggerManager.LogWarn($"Query:LoginQuery - User:{request.Email} with invalid password");

            throw new DataUserBadRequestException(nameof(request.Password));
        } 

        var refreshToken = _jwtGenerator.CreateRefreshToken();
        user.RefreshToken = refreshToken;
            
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            
        await _userManager.UpdateAsync(user);
            
        _loggerManager.LogInfo($"Query:LoginQuery - User:{request.Email} was finished");

        return new AuthenticatedResponse()
        {
            UserId = user.Id,
            Email = user.Email,
            Token = _jwtGenerator.CreateToken(user),
            RefreshToken = user.RefreshToken
        };
    }
}