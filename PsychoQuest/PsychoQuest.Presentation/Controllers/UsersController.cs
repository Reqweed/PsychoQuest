using Application.CommandsQueries.User.Commands.DeleteUser;
using Application.CommandsQueries.User.Queries.GetAllUsers;
using Application.CommandsQueries.User.Queries.GetUser;
using Auth.Commands.RefreshToken;
using Auth.Commands.Registration;
using Auth.Commands.UpdateUser;
using Auth.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;

namespace PsychoQuest.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator, ILoggerManager loggerManager) : base(mediator, loggerManager)
    {
    }
    
    [ResponseCache(CacheProfileName = "Cache")]
    [HttpGet]
    public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:User Action:GetUser - User with id:{UserId} has begun");
        
        var getUserQuery = new GetUserQuery()
        {
            UserId = UserId
        };
        
        var user = await Mediator.Send(getUserQuery, cancellationToken);

        LoggerManager.LogInfo($"Controller:User Action:GetUser - User with id:{UserId} was finished");
        
        return Ok(user);
    }
    
    [ResponseCache(CacheProfileName = "Cache")]
    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUser(CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:User Action:GetAllUser - User with id:{UserId} has begun");

        var getAllUserQuery = new GetAllUsersQuery();
        
        var user = await Mediator.Send(getAllUserQuery, cancellationToken);

        LoggerManager.LogInfo($"Controller:User Action:GetAllUser - User with id:{UserId} was finished");
        
        return Ok(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:User Action:DeleteUser - User with id:{UserId} has begun");

        var deleteUserCommand = new DeleteUserCommand()
        {
            UserId = UserId
        };

        await Mediator.Send(deleteUserCommand, cancellationToken);

        LoggerManager.LogInfo($"Controller:User Action:DeleteUser - User with id:{UserId} was finished");

        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:User Action:Login - User with id:{UserId} has begun");

        var authResponse = await Mediator.Send(loginQuery, cancellationToken);
       
        LoggerManager.LogInfo($"Controller:User Action:Login - User with id:{UserId} was finished");

        return Ok(authResponse);
    }

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationUserCommand registrationUserCommand, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:User Action:Registration - New user has begun");

        var authResponse = await Mediator.Send(registrationUserCommand, cancellationToken);

        LoggerManager.LogInfo($"Controller:User Action:Registration - New user with id:{UserId} was finished");
 
        return Ok(authResponse);
    }
    
    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenCommand refreshTokenCommand, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:User Action:RefreshToken - User with id:{UserId} has begun");

        refreshTokenCommand.UserId = UserId;
        
        var response = await Mediator.Send(refreshTokenCommand, cancellationToken);

        LoggerManager.LogInfo($"Controller:User Action:RefreshToken - User with id:{UserId} was finished");

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:User Action:UpdateUser - User with id:{UserId} has begun");

        await Mediator.Send(updateUserCommand, cancellationToken);

        LoggerManager.LogInfo($"Controller:User Action:UpdateUser - User with id:{UserId} was finished");

        return Ok();
    }
}