using MediatR;

namespace Application.CommandsQueries.User.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<IEnumerable<Entities.Models.User>>
{
    
}