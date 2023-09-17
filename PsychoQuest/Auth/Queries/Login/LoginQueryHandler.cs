using Auth.Interfaces;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery,AuthenticatedResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;


    public LoginQueryHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<AuthenticatedResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) 
                   ?? throw new Exception();//fix exception
        
        var result = await _signInManager
            .CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded) 
            throw new Exception();//fix exception

        var refreshToken = _jwtGenerator.CreateRefreshToken();
        user.RefreshToken = refreshToken;
            
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            
        await _userManager.UpdateAsync(user);
            
        return new AuthenticatedResponse()
        {
            UserId = user.Id,

            Email = user.Email,
            Token = _jwtGenerator.CreateToken(user),
            RefreshToken = user.RefreshToken
        };
    }
}