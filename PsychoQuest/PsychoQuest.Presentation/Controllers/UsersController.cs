using Application.CommandsQueries.User.Commands.DeleteUser;
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
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        LoggerManager.LogInfo($"Controller:User Action:GetUser - User with id:{UserId} has begun");
        
        var getUserQuery = new GetUserQuery()
        {
            UserId = UserId
        };
        
        var user = await Mediator.Send(getUserQuery);

        LoggerManager.LogInfo($"Controller:User Action:GetUser - User with id:{UserId} was finished");
        
        return Ok(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser()
    {
        LoggerManager.LogInfo($"Controller:User Action:DeleteUser - User with id:{UserId} has begun");

        var deleteUserCommand = new DeleteUserCommand()
        {
            UserId = UserId
        };

        await Mediator.Send(deleteUserCommand);

        LoggerManager.LogInfo($"Controller:User Action:DeleteUser - User with id:{UserId} was finished");

        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
    {
        LoggerManager.LogInfo($"Controller:User Action:Login - User with id:{UserId} has begun");

        var authResponse = await Mediator.Send(loginQuery);
       
        LoggerManager.LogInfo($"Controller:User Action:Login - User with id:{UserId} was finished");

        return Ok(authResponse);
    }

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationUserCommand registrationUserCommand)
    {
        LoggerManager.LogInfo($"Controller:User Action:Registration - New user has begun");

        var authResponse = await Mediator.Send(registrationUserCommand);

        LoggerManager.LogInfo($"Controller:User Action:Registration - New user with id:{UserId} was finished");
 
        return Ok(authResponse);
    }
    
    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenCommand refreshTokenCommand)
    {
        LoggerManager.LogInfo($"Controller:User Action:RefreshToken - User with id:{UserId} has begun");

        refreshTokenCommand.UserId = UserId;
        
        var response = await Mediator.Send(refreshTokenCommand);

        LoggerManager.LogInfo($"Controller:User Action:RefreshToken - User with id:{UserId} was finished");

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
    {
        LoggerManager.LogInfo($"Controller:User Action:UpdateUser - User with id:{UserId} has begun");

        await Mediator.Send(updateUserCommand);

        LoggerManager.LogInfo($"Controller:User Action:UpdateUser - User with id:{UserId} was finished");

        return Ok();
    }
}