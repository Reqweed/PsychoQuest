using Entities.Exceptions.NotFoundException;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.Answer.Commands.DeleteAnswers;

public class DeleteTestAnswersCommandHandler : IRequestHandler<DeleteTestAnswersCommand>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public DeleteTestAnswersCommandHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public async Task Handle(DeleteTestAnswersCommand request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Command:DeleteTestAnswersCommand - Answers for user:{request.UserId} and test:{request.TypeTest} has begun");

        if (await _repositoryManager.TestAnswers.TestAnswersExistsAsync(request.UserId, request.TypeTest, cancellationToken))
        {
            _loggerManager.LogWarn($"Command:DeleteTestAnswersCommand - Answers for user:{request.UserId} and test:{request.TypeTest} doesn't exist");

            throw new TestAnswersNotFoundException(request.UserId, request.TypeTest);
        }

        await _repositoryManager.TestAnswers.DeleteTestAnswersAsync(request.UserId, request.TypeTest, cancellationToken);
        
        _loggerManager.LogInfo($"Command:DeleteTestAnswersCommand - Answers for user:{request.UserId} and test:{request.TypeTest} was finished");
    }
}