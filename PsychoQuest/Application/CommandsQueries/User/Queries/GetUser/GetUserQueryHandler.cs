using Entities.Exceptions.NotFoundException;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.User.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery,Entities.Models.User>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public GetUserQueryHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public async Task<Entities.Models.User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetUserQuery - User with id:{request.UserId} has begun");

        var user = await _repositoryManager.User.GetUserAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            _loggerManager.LogWarn($"Query:GetUserQuery - User with id:{request.UserId} doesn't exist");

            throw new UserNotFoundException(request.UserId);
        }

        _loggerManager.LogInfo($"Query:GetUserQuery - User with id:{request.UserId} was finished");

        return user;
    }
}