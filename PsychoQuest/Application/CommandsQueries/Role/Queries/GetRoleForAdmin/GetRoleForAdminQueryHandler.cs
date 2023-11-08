using Application.Caches.Implementations;
using Application.Caches.Interfaces;
using Entities.Exceptions.NotFoundException;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Application.CommandsQueries.Role.Queries.GetRoleForAdmin;

public class GetRoleForAdminQueryHandler : IRequestHandler<GetRoleForAdminQuery,IdentityRole<long>>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly ILoggerManager _loggerManager;
    private readonly ICacheManager<IdentityRole<long>> _cacheManager;

    public GetRoleForAdminQueryHandler(RoleManager<IdentityRole<long>> roleManager, ILoggerManager loggerManager, ICacheManager<IdentityRole<long>> cacheManager)
    {
        _roleManager = roleManager;
        _loggerManager = loggerManager;
        _cacheManager = cacheManager;
    }

    public async Task<IdentityRole<long>> Handle(GetRoleForAdminQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetRoleForAdminQuery - Role with id:{request.RoleId} has begun");

        var roleFunc = async() => await _roleManager.FindByIdAsync(request.RoleId.ToString());

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var role = await _cacheManager.GetOrSetCacheValue("role-admin", roleFunc);
        if (role is null)
        {
            _loggerManager.LogWarn($"Query:GetRoleForAdminQuery - Role with id:{request.RoleId} doesn't exist");
            
            throw new RoleNotFoundException(request.RoleId);
        }

        _loggerManager.LogInfo($"Query:GetRoleForAdminQuery - Role with id:{request.RoleId} was finished");

        return role;
    }
}