using Application.Caches.Implementations;
using Application.Caches.Interfaces;
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
    private readonly ICacheManager<List<IdentityRole<long>>> _cacheManager;
    
    public GetAllRolesQueryHandler(RoleManager<IdentityRole<long>> roleManager, ILoggerManager loggerManager, ICacheManager<List<IdentityRole<long>>> cacheManager)
    {
        _roleManager = roleManager;
        _loggerManager = loggerManager;
        _cacheManager = cacheManager;
    }

    public async Task<List<IdentityRole<long>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetAllRolesQuery has begun");

        
        var roleFunc = async () => await _roleManager.Roles.ToListAsync(cancellationToken: cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var roles = await _cacheManager.GetOrSetCacheValue("all-role", roleFunc);
        
        if (roles is null)
        {
            _loggerManager.LogWarn($"Query:GetAllRolesQuery - Roles doesn't exist");
            
            throw new RoleNotFoundException();
        } 
        
        _loggerManager.LogInfo($"Query:GetAllRolesQuery was finished");

        return roles;
    }
}