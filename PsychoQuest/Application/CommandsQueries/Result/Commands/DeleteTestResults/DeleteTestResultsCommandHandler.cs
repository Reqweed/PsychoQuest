using Entities.Exceptions.NotFoundException;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.Result.Commands.DeleteTestResults;

public class DeleteTestResultsCommandHandler : IRequestHandler<DeleteTestResultsCommand>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public DeleteTestResultsCommandHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public async Task Handle(DeleteTestResultsCommand request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Command:DeleteTestResults - User with id:{request.UserId} and Test:{request.TypeTest} has begun");

        if (await _repositoryManager.TestResults.TestResultExistsAsync(request.UserId, request.TypeTest))
        {
            _loggerManager.LogWarn($"Command:DeleteTestResults - User with id:{request.UserId} and Test:{request.TypeTest} doesn't exist");
   
            throw new TestResultsNotFoundException(request.UserId, request.TypeTest);
        }
        
        await _repositoryManager.TestResults.DeleteTestResultsAsync(request.UserId, request.TypeTest);
        
        _loggerManager.LogInfo($"Command:DeleteTestResults - User with id:{request.UserId} and Test:{request.TypeTest} was finished");
    }
}