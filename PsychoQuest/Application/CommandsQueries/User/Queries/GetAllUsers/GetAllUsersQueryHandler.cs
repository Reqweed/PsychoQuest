using Application.Caches.Implementations;
using Application.Caches.Interfaces;
using AutoMapper;
using Entities.DataTransferObjects.UserDto;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.User.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery,IEnumerable<UserWithIdDto>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    private readonly ICacheManager<IEnumerable<Entities.Models.User>> _cacheManager;

    public GetAllUsersQueryHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager, ICacheManager<IEnumerable<Entities.Models.User>> cacheManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _cacheManager = cacheManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserWithIdDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetAllUsersQuery has begun");
        
        var usersFunc = async() => await _repositoryManager.User.GetAllUsers(cancellationToken);

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var users = await _cacheManager.GetOrSetCacheValue("All-users", usersFunc);

        var usersDto = _mapper.Map<IEnumerable<UserWithIdDto>>(users);
        
        _loggerManager.LogInfo($"Query:GetAllUsersQuery was finished");
        
        return usersDto;
    }
}