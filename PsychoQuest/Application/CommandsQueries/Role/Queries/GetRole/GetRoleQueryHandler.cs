using Entities.Exceptions.NotFoundException;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Application.CommandsQueries.Role.Queries.GetRole;

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery,string>
{
    private readonly UserManager<Entities.Models.User> _userManager;
    private readonly IRepositoryManager _repositoryManager;

    public GetRoleQueryHandler(UserManager<Entities.Models.User> userManager, IRepositoryManager repositoryManager)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
    }

    public async Task<string> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var user = await _repositoryManager.User.GetUserAsync(request.UserId) 
                   ?? throw new UserNotFoundException(request.UserId);

        var role = await _userManager.GetRolesAsync(user)
                   ?? throw new RoleNotFoundException(user.Id);

        return role[0];
    }
}