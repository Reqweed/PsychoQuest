using MediatR;

namespace Application.CommandsQueries.User.Queries.GetUser;

public class GetUserQuery : IRequest<Entities.Models.User>
{
    public long UserId { get; set; }
}