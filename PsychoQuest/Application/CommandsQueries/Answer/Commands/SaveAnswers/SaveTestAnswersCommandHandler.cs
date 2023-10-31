using Entities.Enums;
using Entities.Exceptions.NotFoundException;
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

        if (!await _repositoryManager.TestResults.TestResultExistsAsync(request.TestAnswers.UserId, request.TestAnswers.TestName))
        {
            _loggerManager.LogWarn($"Command:SaveTestAnswersCommand - Result for user:{request.TestAnswers.UserId} and test:{request.TestAnswers.TestName} doesn't exist");

            await _repositoryManager.TestResults.DeleteTestResultsAsync(request.TestAnswers.UserId, request.TestAnswers.TestName);
        }

        if (!await _repositoryManager.TestAnswers.TestAnswersExistsAsync(request.TestAnswers.UserId, request.TestAnswers.TestName))
        {
            _loggerManager.LogWarn($"Command:SaveTestAnswersCommand - Answers for user:{request.TestAnswers.UserId} and test:{request.TestAnswers.TestName} doesn't exist");

            await _repositoryManager.TestAnswers.DeleteTestAnswersAsync(request.TestAnswers.UserId,request.TestAnswers.TestName);
        }
        
        var result = request.TestAnswers.TestName switch
        {
            TypeTest.ScaleBeck => _serviceManager.ScaleBeck.CalculateTest(request.TestAnswers),
            TypeTest.TestHall => _serviceManager.TestHall.CalculateTest(request.TestAnswers),
            _ => throw new TypeTestNotFoundException(request.TestAnswers.TestName)//add log
        };
        
        result = _serviceManager
        
        await _repositoryManager.TestAnswers.SaveTestAnswersAsync(request.TestAnswers);
        await _repositoryManager.TestResults.SaveTestResultsAsync(result);
        
        _loggerManager.LogInfo($"Command:SaveTestAnswersCommand - Answers for user:{request.TestAnswers.UserId} and test:{request.TestAnswers.TestName} was finished");
    }
}