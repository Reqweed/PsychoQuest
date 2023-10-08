using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandsQueries.Role.Queries.GetRoleForAdmin;

public class GetRoleForAdminQuery : IRequest<IdentityRole<long>>
{
    public long RoleId { get; set; }
}