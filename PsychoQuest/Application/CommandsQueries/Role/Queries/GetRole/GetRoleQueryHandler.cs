using Entities.Exceptions.NotFoundException;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Application.CommandsQueries.Role.Queries.GetRole;

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery,string>
{
    private readonly UserManager<Entities.Models.User> _userManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public GetRoleQueryHandler(UserManager<Entities.Models.User> userManager, IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public async Task<string> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetRoleQuery - User with id:{request.UserId} has begun");

        var user = await _repositoryManager.User.GetUserAsync(request.UserId);
        if (user is null)
        {
            _loggerManager.LogWarn($"Query:GetRoleQuery - User with id:{request.UserId} doesn't exist");

            throw new UserNotFoundException(request.UserId);
        }
        
        var role = await _userManager.GetRolesAsync(user);
        if (role is null)
        {
            _loggerManager.LogWarn($"Query:GetRoleQuery - User with id:{request.UserId} hadn't role");

            throw new RoleNotFoundException(user.Id);
        }

        _loggerManager.LogInfo($"Query:GetRoleQuery - User with id:{request.UserId} was finished");

        return role[0];
    }
}