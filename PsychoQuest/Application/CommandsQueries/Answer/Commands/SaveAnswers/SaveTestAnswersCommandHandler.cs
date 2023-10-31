using Amazon.Runtime.Internal.Transform;
using Entities.Enums;
using Entities.Exceptions.NotFoundException;
using Entities.Models;
using MediatR;
using Repository.Contracts;
using Service.Contracts;

namespace Application.CommandsQueries.Answer.Commands.SaveAnswers;

public class SaveTestAnswersCommandHandler : IRequestHandler<SaveTestAnswersCommand>
{
    private readonly IServiceManager _serviceManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public SaveTestAnswersCommandHandler(IServiceManager serviceManager, IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public async Task Handle(SaveTestAnswersCommand request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Command:SaveTestAnswersCommand - Answers for user:{request.TestAnswers.UserId} and test:{request.TestAnswers.TestName} has begun");

        if (!await _repositoryManager.TestResults.TestResultExistsAsync(request.TestAnswers.UserId, request.TestAnswers.TestName, cancellationToken))
        {
            _loggerManager.LogWarn($"Command:SaveTestAnswersCommand - Result for user:{request.TestAnswers.UserId} and test:{request.TestAnswers.TestName} doesn't exist");

            await _repositoryManager.TestResults.DeleteTestResultsAsync(request.TestAnswers.UserId, request.TestAnswers.TestName, cancellationToken);
        }

        if (!await _repositoryManager.TestAnswers.TestAnswersExistsAsync(request.TestAnswers.UserId, request.TestAnswers.TestName, cancellationToken))
        {
            _loggerManager.LogWarn($"Command:SaveTestAnswersCommand - Answers for user:{request.TestAnswers.UserId} and test:{request.TestAnswers.TestName} doesn't exist");

            await _repositoryManager.TestAnswers.DeleteTestAnswersAsync(request.TestAnswers.UserId,request.TestAnswers.TestName, cancellationToken);
        }

        var result = _serviceManager.Calculate(request.TestAnswers);

        if (result is null)
        {
            _loggerManager.LogWarn($"Command:SaveTestAnswersCommand - Calculate for user:{request.TestAnswers.UserId} and test:{request.TestAnswers.TestName} has been error");

            throw new TypeTestNotFoundException(request.TestAnswers.TestName);
        }
        
        await _repositoryManager.TestAnswers.SaveTestAnswersAsync(request.TestAnswers, cancellationToken);
        await _repositoryManager.TestResults.SaveTestResultsAsync(result, cancellationToken);
        
        _loggerManager.LogInfo($"Command:SaveTestAnswersCommand - Answers for user:{request.TestAnswers.UserId} and test:{request.TestAnswers.TestName} was finished");
    }
}