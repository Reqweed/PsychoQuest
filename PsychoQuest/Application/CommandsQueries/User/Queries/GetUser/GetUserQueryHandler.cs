using Application.Caches.Implementations;
using Application.Caches.Interfaces;
using AutoMapper;
using Entities.DataTransferObjects.UserDto;
using Entities.Exceptions.NotFoundException;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.User.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery,UserDto>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Entities.Models.User> _cacheManager;

    public GetUserQueryHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager, ICacheManager<Entities.Models.User> cacheManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _cacheManager = cacheManager;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
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

        var userDto = _mapper.Map<UserDto>(user);
        
        _loggerManager.LogInfo($"Query:GetUserQuery - User with id:{request.UserId} was finished");

        return userDto;
    }
}