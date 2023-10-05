using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandsQueries.Role.Queries.GetRole;

public class GetRoleQueries : IRequest<IdentityRole<long>>
{
    public long RoleId { get; set; }
}