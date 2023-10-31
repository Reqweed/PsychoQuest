using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.User.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery,IEnumerable<Entities.Models.User>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public GetAllUsersQueryHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public async Task<IEnumerable<Entities.Models.User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetAllUsersQuery has begun");
        
        var users = await _repositoryManager.User.GetAllUsers(cancellationToken);

        _loggerManager.LogInfo($"Query:GetAllUsersQuery was finished");
        
        return users;
    }
}