using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandsQueries.Role.Queries.GetAllRoles;

public class GetAllRolesQueries : IRequest<List<IdentityRole<long>>>
{
    
}