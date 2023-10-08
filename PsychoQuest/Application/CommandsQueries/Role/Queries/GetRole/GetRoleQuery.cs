using MediatR;

namespace Application.CommandsQueries.Role.Queries.GetRole;

public class GetRoleQuery : IRequest<string>
{
    public long UserId { get; set;}
}