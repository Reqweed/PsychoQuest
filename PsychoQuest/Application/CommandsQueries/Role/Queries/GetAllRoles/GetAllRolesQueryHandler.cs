using Entities.Exceptions.NotFoundException;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Application.CommandsQueries.Role.Queries.GetAllRoles;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery,List<IdentityRole<long>>>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly ILoggerManager _loggerManager;
    
    public GetAllRolesQueryHandler(RoleManager<IdentityRole<long>> roleManager, ILoggerManager loggerManager)
    {
        _roleManager = roleManager;
        _loggerManager = loggerManager;
    }

    public async Task<List<IdentityRole<long>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetAllRolesQuery has begun");

        var roles = await _roleManager.Roles.ToListAsync();
        
        if (roles is null)
        {
            _loggerManager.LogWarn($"Query:GetAllRolesQuery - Roles doesn't exist");
            
            throw new RoleNotFoundException();
        } 
        
        _loggerManager.LogInfo($"Query:GetAllRolesQuery was finished");

        return roles;
    }
}