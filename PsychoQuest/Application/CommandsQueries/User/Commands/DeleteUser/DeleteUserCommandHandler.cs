using Entities.Exceptions.NotFoundException;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.User.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public DeleteUserCommandHandler(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;
    
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (await _repositoryManager.User.UserExistsAsync(request.UserId)) 
            throw new UserNotFoundException(request.UserId);

        await _repositoryManager.User.DeleteUserAsync(request.UserId);
    }
}