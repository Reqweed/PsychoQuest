using Entities.Exceptions.NotFoundException;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.User.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public DeleteUserCommandHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Command:DeleteUserCommand - User with id:{request.UserId} has begun");

        if (await _repositoryManager.User.UserExistsAsync(request.UserId))
        {
            _loggerManager.LogWarn($"Command:DeleteUserCommand - User with id:{request.UserId} doesn't exist");
            
            throw new UserNotFoundException(request.UserId);
        } 

        await _repositoryManager.User.DeleteUserAsync(request.UserId);
        
        _loggerManager.LogInfo($"Command:DeleteUserCommand - User with id:{request.UserId} was finished");
    }
}