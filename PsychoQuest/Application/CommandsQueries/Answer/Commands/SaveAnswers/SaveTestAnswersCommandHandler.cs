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

    public SaveTestAnswersCommandHandler(IServiceManager serviceManager, IRepositoryManager repositoryManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
    }

    public async Task Handle(SaveTestAnswersCommand request, CancellationToken cancellationToken)
    {
        if (!await _repositoryManager.TestResults.TestResultExistsAsync(request.TestAnswers.UserId, request.TestAnswers.TestName))
            await _repositoryManager.TestResults.DeleteTestResultsAsync(request.TestAnswers.UserId, request.TestAnswers.TestName);
        
        if (!await _repositoryManager.TestAnswers.TestAnswersExistsAsync(request.TestAnswers.UserId, request.TestAnswers.TestName)) 
            await _repositoryManager.TestAnswers.DeleteTestAnswersAsync(request.TestAnswers.UserId,request.TestAnswers.TestName);
        
        var result = request.TestAnswers.TestName switch
        {
            TypeTest.ScaleBeck => _serviceManager.ScaleBeck.CalculateForScaleBeck(request.TestAnswers),
            TypeTest.TestHall => _serviceManager.TestHall.CalculateForTestHall(request.TestAnswers),
            _ => throw new TypeTestNotFoundException(request.TestAnswers.TestName)
        };
        
        await _repositoryManager.TestAnswers.SaveTestAnswersAsync(request.TestAnswers);
        await _repositoryManager.TestResults.SaveTestResultsAsync(result);
    }
}