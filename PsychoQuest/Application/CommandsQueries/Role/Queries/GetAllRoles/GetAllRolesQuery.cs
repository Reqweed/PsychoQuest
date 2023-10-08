using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandsQueries.Role.Queries.GetAllRoles;

public class GetAllRolesQuery : IRequest<List<IdentityRole<long>>>
{
    
}