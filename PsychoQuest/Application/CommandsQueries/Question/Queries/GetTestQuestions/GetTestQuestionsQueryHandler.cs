using Application.Caches.Implementations;
using Application.Caches.Interfaces;
using Entities.Exceptions.NotFoundException;
using Entities.Models;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.Question.Queries.GetTestQuestions;

public class GetTestQuestionsQueryHandler : IRequestHandler<GetTestQuestionsQuery,TestQuestions>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly ICacheManager<TestQuestions> _cacheManager;

    public GetTestQuestionsQueryHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager, ICacheManager<TestQuestions> cacheManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _cacheManager = cacheManager;
    }

    public async Task<TestQuestions> Handle(GetTestQuestionsQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetTestQuestionsQuery - Questions:{request.TypeTest} has begun");

        var questionsFunc = async() => await _repositoryManager.TestQuestions.GetTestQuestionsAsync(request.TypeTest, cancellationToken);

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var questions = await _cacheManager.GetOrSetCacheValue("questions", questionsFunc);
        if (questions is null)
        {
            _loggerManager.LogWarn($"Query:GetTestQuestionsQuery - Questions:{request.TypeTest} doesn't exist");

            throw new TestQuestionsNotFoundException(request.TypeTest);
        }

        _loggerManager.LogInfo($"Query:GetTestQuestionsQuery - Questions:{request.TypeTest} was finished");

        return questions;
    }
}