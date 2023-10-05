using Application.CommandsQueries.Role.Commands.CreateRole;
using Application.CommandsQueries.Role.Commands.DeleteRole;
using Application.CommandsQueries.Role.Commands.SetRole;
using Application.CommandsQueries.Role.Queries.GetAllRoles;
using Application.CommandsQueries.Role.Queries.GetRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsychoQuest.Presentation.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class RolesController : BaseController
{
    public RolesController(IMediator mediator) : base(mediator) {}
    
    [HttpGet("{roleId:long}")]
    public async Task<IActionResult> GetRole(long roleId)
    {
        var getRoleQueries = new GetRoleQueries()
        {
            RoleId = roleId
        };

        var role = await Mediator.Send(getRoleQueries);

        return Ok(role);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var getAllRolesQueries = new GetAllRolesQueries();

        var roles = await Mediator.Send(getAllRolesQueries);

        return Ok(roles);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand createRoleCommand)
    {
        await Mediator.Send(createRoleCommand);

        return Ok();
    }

    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteRole(long roleId)
    {
        var deleteRoleCommand = new DeleteRoleCommand()
        {
            RoleId = roleId
        };

        await Mediator.Send(deleteRoleCommand);

        return Ok();
    }

    [HttpPost("set-role")]
    public async Task<IActionResult> SetRole([FromBody] long roleId)
    {
        var setRoleCommand = new SetRoleCommand()
        {
            UserId = UserId,
            RoleId = roleId
        };

        await Mediator.Send(setRoleCommand);

        return Ok();
    }
}