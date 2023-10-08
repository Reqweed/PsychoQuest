using Entities.Exceptions.NotFoundException;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandsQueries.Role.Queries.GetRoleForAdmin;

public class GetRoleForAdminQueryHandler : IRequestHandler<GetRoleForAdminQuery,IdentityRole<long>>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public GetRoleForAdminQueryHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IdentityRole<long>> Handle(GetRoleForAdminQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString()) 
                   ?? throw new RoleNotFoundException(request.RoleId);

        return role;
    }
}