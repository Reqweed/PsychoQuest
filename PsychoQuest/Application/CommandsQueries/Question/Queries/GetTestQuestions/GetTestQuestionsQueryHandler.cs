using Entities.Exceptions.NotFoundException;
using Entities.Models;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.Question.Queries.GetTestQuestions;

public class GetTestQuestionsQueryHandler : IRequestHandler<GetTestQuestionsQuery,TestQuestions>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public GetTestQuestionsQueryHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public async Task<TestQuestions> Handle(GetTestQuestionsQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetTestQuestionsQuery - Questions:{request.TypeTest} has begun");

        var questions = await _repositoryManager.TestQuestions.GetTestQuestionsAsync(request.TypeTest);

        if (questions is null)
        {
            _loggerManager.LogWarn($"Query:GetTestQuestionsQuery - Questions:{request.TypeTest} doesn't exist");

            throw new TestQuestionsNotFoundException(request.TypeTest);
        }

        _loggerManager.LogInfo($"Query:GetTestQuestionsQuery - Questions:{request.TypeTest} was finished");

        return questions;
    }
}