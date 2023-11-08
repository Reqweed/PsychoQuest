using Application.Caches.Implementations;
using Application.Caches.Interfaces;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.User.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery,IEnumerable<Entities.Models.User>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly ICacheManager<IEnumerable<Entities.Models.User>> _cacheManager;

    public GetAllUsersQueryHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager, ICacheManager<IEnumerable<Entities.Models.User>> cacheManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _cacheManager = cacheManager;
    }

    public async Task<IEnumerable<Entities.Models.User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetAllUsersQuery has begun");
        
        var usersFunc = async() => await _repositoryManager.User.GetAllUsers(cancellationToken);

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var users = await _cacheManager.GetOrSetCacheValue("All-users", usersFunc);
            
        _loggerManager.LogInfo($"Query:GetAllUsersQuery was finished");
        
        return users;
    }
}