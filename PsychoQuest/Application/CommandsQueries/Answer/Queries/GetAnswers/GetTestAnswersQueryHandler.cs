using Entities.Exceptions.NotFoundException;
using Entities.Models;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.Answer.Queries.GetAnswers;

public class GetTestAnswersQueryHandler : IRequestHandler<GetTestAnswersQuery,TestAnswers>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public GetTestAnswersQueryHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public async Task<TestAnswers> Handle(GetTestAnswersQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetTestAnswersQuery - Answers for user:{request.UserId} and test:{request.TypeTest} has begun");

        if (await _repositoryManager.TestAnswers.TestAnswersExistsAsync(request.UserId, request.TypeTest))
        {
            _loggerManager.LogWarn($"Query:GetTestAnswersQuery - Answers for user:{request.UserId} and test:{request.TypeTest} doesn't exist");

            throw new TestAnswersNotFoundException(request.UserId, request.TypeTest);
        }

        var answers = await _repositoryManager.TestAnswers.GetTestAnswersAsync(request.UserId, request.TypeTest);

        _loggerManager.LogInfo($"Query:GetTestAnswersQuery - Answers for user:{request.UserId} and test:{request.TypeTest} was finished");

        return answers;
    }
}