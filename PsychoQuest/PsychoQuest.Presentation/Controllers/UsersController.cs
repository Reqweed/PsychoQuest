using Application.CommandsQueries.User.Commands.DeleteUser;
using Application.CommandsQueries.User.Queries.GetUser;
using Auth.Commands.RefreshToken;
using Auth.Commands.Registration;
using Auth.Commands.UpdateUser;
using Auth.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace PsychoQuest.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        var getUserQuery = new GetUserQuery()
        {
            UserId = UserId
        };
        
        var user = await Mediator.Send(getUserQuery);

        return Ok(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser()
    {
        var deleteUserCommand = new DeleteUserCommand()
        {
            UserId = UserId
        };

        await Mediator.Send(deleteUserCommand);

        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
    {
        var authResponse = await Mediator.Send(loginQuery);
        
        return Ok(authResponse);
    }

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationUserCommand registrationUserCommand)
    {
        var authResponse = await Mediator.Send(registrationUserCommand);

        return Ok(authResponse);
    }
    
    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenCommand refreshTokenCommand)
    {
        var response = await Mediator.Send(refreshTokenCommand);

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
    {
        await Mediator.Send(updateUserCommand);

        return Ok();
    }
}