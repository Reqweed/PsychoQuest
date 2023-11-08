using Application.Caches.Implementations;
using Application.Caches.Interfaces;
using Entities.Exceptions.NotFoundException;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.User.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery,Entities.Models.User>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly ICacheManager<Entities.Models.User> _cacheManager;

    public GetUserQueryHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager, ICacheManager<Entities.Models.User> cacheManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _cacheManager = cacheManager;
    }

    public async Task<Entities.Models.User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetUserQuery - User with id:{request.UserId} has begun");

        var userFunc = async() => await _repositoryManager.User.GetUserAsync(request.UserId, cancellationToken);

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var user = await _cacheManager.GetOrSetCacheValue("user", userFunc);
        if (user is null)
        {
            _loggerManager.LogWarn($"Query:GetUserQuery - User with id:{request.UserId} doesn't exist");

            throw new UserNotFoundException(request.UserId);
        }

        _loggerManager.LogInfo($"Query:GetUserQuery - User with id:{request.UserId} was finished");

        return user;
    }
}