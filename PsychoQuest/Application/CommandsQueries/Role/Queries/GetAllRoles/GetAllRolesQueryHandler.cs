using Entities.Exceptions.NotFoundException;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsQueries.Role.Queries.GetAllRoles;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery,List<IdentityRole<long>>>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public GetAllRolesQueryHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<List<IdentityRole<long>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync() 
                    ?? throw new RoleNotFoundException();
        
        return roles;
    }
}