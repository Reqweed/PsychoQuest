using Entities.DataTransferObjects.UserDto;
using MediatR;

namespace Application.CommandsQueries.User.Queries.GetUser;

public class GetUserQuery : IRequest<UserDto>
{
    public long UserId { get; set; }
}