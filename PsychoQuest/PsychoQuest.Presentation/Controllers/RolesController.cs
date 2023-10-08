using Application.CommandsQueries.Role.Commands.CreateRole;
using Application.CommandsQueries.Role.Commands.DeleteRole;
using Application.CommandsQueries.Role.Commands.SetRole;
using Application.CommandsQueries.Role.Queries.GetAllRoles;
using Application.CommandsQueries.Role.Queries.GetRole;
using Application.CommandsQueries.Role.Queries.GetRoleForAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsychoQuest.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class RolesController : BaseController
{
    public RolesController(IMediator mediator) : base(mediator) {}
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{roleId:long}")]
    public async Task<IActionResult> GetRoleForAdmin(long roleId)
    {
        var getRoleForAdmin = new GetRoleForAdminQuery()
        {
            RoleId = roleId
        };

        var role = await Mediator.Send(getRoleForAdmin);

        return Ok(role);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRole()
    {
        var getRoleQueries = new GetRoleQuery()
        {
            UserId = UserId
        };

        var role = await Mediator.Send(getRoleQueries);

        return Ok(role);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllRoles()
    {
        var getAllRolesQueries = new GetAllRolesQuery();

        var roles = await Mediator.Send(getAllRolesQueries);

        return Ok(roles);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand createRoleCommand)
    {
        await Mediator.Send(createRoleCommand);

        return Ok();
    }

    [Authorize(Roles = "Admin")]
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
    
    [Authorize(Roles = "Admin")]
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