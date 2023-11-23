using Entities.DataTransferObjects.UserDto;
using MediatR;

namespace Application.CommandsQueries.User.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<IEnumerable<UserWithIdDto>>
{
    
}