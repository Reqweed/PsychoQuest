using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsQueries.Role.Queries.GetAllRoles;

public class GetAllRolesQueriesHandler : IRequestHandler<GetAllRolesQueries,List<IdentityRole<long>>>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public GetAllRolesQueriesHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<List<IdentityRole<long>>> Handle(GetAllRolesQueries request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync() 
                    ?? throw new Exception();//fix
        
        return roles;
    }
}