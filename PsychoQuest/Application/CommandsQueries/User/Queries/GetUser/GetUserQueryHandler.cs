using Entities.Exceptions.NotFoundException;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.User.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery,Entities.Models.User>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetUserQueryHandler(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public async Task<Entities.Models.User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _repositoryManager.User.GetUserAsync(request.UserId) 
                   ?? throw new UserNotFoundException(request.UserId);

        return user;
    }
}