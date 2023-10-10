using Entities.Exceptions.NotFoundException;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Application.CommandsQueries.Role.Queries.GetRoleForAdmin;

public class GetRoleForAdminQueryHandler : IRequestHandler<GetRoleForAdminQuery,IdentityRole<long>>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly ILoggerManager _loggerManager;

    public GetRoleForAdminQueryHandler(RoleManager<IdentityRole<long>> roleManager, ILoggerManager loggerManager)
    {
        _roleManager = roleManager;
        _loggerManager = loggerManager;
    }

    public async Task<IdentityRole<long>> Handle(GetRoleForAdminQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetRoleForAdminQuery - Role with id:{request.RoleId} has begun");

        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is null)
        {
            _loggerManager.LogWarn($"Query:GetRoleForAdminQuery - Role with id:{request.RoleId} doesn't exist");
            
            throw new RoleNotFoundException(request.RoleId);
        }

        _loggerManager.LogInfo($"Query:GetRoleForAdminQuery - Role with id:{request.RoleId} was finished");

        return role;
    }
}