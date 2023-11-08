using Application.CommandsQueries.Role.Commands.CreateRole;
using Application.CommandsQueries.Role.Commands.DeleteRole;
using Application.CommandsQueries.Role.Commands.SetRole;
using Application.CommandsQueries.Role.Queries.GetAllRoles;
using Application.CommandsQueries.Role.Queries.GetRole;
using Application.CommandsQueries.Role.Queries.GetRoleForAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;

namespace PsychoQuest.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class RolesController : BaseController
{
    public RolesController(IMediator mediator, ILoggerManager loggerManager) : base(mediator, loggerManager)
    {
    }
    
    [Authorize(Roles = "Admin")]
    [ResponseCache(CacheProfileName = "Cache")]
    [HttpGet("{roleId:long}")]
    public async Task<IActionResult> GetRoleForAdmin(long roleId, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:Roles Action:GetRoleForAdmin - User with id:{UserId} has begun");

        var getRoleForAdmin = new GetRoleForAdminQuery()
        {
            RoleId = roleId
        };

        var role = await Mediator.Send(getRoleForAdmin, cancellationToken);

        LoggerManager.LogInfo($"Controller:Roles Action:GetRoleForAdmin - User with id:{UserId} was finished");

        return Ok(role);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRole(CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:Roles Action:GetRole - User with id:{UserId} has begun");

        var getRoleQueries = new GetRoleQuery()
        {
            UserId = UserId
        };

        var role = await Mediator.Send(getRoleQueries, cancellationToken);

        LoggerManager.LogInfo($"Controller:Roles Action:GetRole - User with id:{UserId} was finished");

        return Ok(role);
    }
    
    [Authorize(Roles = "Admin")]
    [ResponseCache(CacheProfileName = "Cache")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:Roles Action:GetAllRoles - User with id:{UserId} has begun");

        var getAllRolesQueries = new GetAllRolesQuery();

        var roles = await Mediator.Send(getAllRolesQueries, cancellationToken);

        LoggerManager.LogInfo($"Controller:Roles Action:GetAllRoles - User with id:{UserId} was finished");

        return Ok(roles);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand createRoleCommand, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:Roles Action:CreateRole - User with id:{UserId} has begun");

        await Mediator.Send(createRoleCommand, cancellationToken);

        LoggerManager.LogInfo($"Controller:Roles Action:CreateRole - User with id:{UserId} was finished");

        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteRole(long roleId, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:Roles Action:DeleteRole - User with id:{UserId} has begun");

        var deleteRoleCommand = new DeleteRoleCommand()
        {
            RoleId = roleId
        };

        await Mediator.Send(deleteRoleCommand, cancellationToken);

        LoggerManager.LogInfo($"Controller:Roles Action:DeleteRole - User with id:{UserId} was finished");

        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("set-role")]
    public async Task<IActionResult> SetRole([FromBody] long roleId, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:Roles Action:SetRole - User with id:{UserId} has begun");

        var setRoleCommand = new SetRoleCommand()
        {
            UserId = UserId,
            RoleId = roleId
        };

        await Mediator.Send(setRoleCommand, cancellationToken);

        LoggerManager.LogInfo($"Controller:Roles Action:SetRole - User with id:{UserId} was finished");

        return Ok();
    }
}