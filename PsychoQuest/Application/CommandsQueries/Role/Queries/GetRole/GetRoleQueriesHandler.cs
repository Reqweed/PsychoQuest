using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandsQueries.Role.Queries.GetRole;

public class GetRoleQueriesHandler : IRequestHandler<GetRoleQueries,IdentityRole<long>>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public GetRoleQueriesHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IdentityRole<long>> Handle(GetRoleQueries request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString()) 
                   ?? throw new Exception();//fix

        return role;
    }
}