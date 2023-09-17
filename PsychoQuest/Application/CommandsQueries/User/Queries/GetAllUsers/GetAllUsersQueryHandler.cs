using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.User.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery,IEnumerable<Entities.Models.User>>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetAllUsersQueryHandler(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;
    
    public async Task<IEnumerable<Entities.Models.User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users =  _repositoryManager.User.GetAllUsers();//fix async

        return users;
    }
}