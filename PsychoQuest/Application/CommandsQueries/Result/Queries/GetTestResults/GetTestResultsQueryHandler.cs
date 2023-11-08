using Application.Caches.Implementations;
using Application.Caches.Interfaces;
using Entities.Exceptions.NotFoundException;
using Entities.Models;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.Result.Queries.GetTestResults;

public class GetTestResultsQueryHandler : IRequestHandler<GetTestResultsQuery,TestResults>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly ICacheManager<TestResults> _cacheManager;
    
    public GetTestResultsQueryHandler(IRepositoryManager repositoryManager, ILoggerManager loggerManager, ICacheManager<TestResults> cacheManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _cacheManager = cacheManager;
    }

    public async Task<TestResults> Handle(GetTestResultsQuery request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Query:GetTestResultsQuery - User with id:{request.UserId} and Test:{request.TypeTest} has begun");
        
        if (await _repositoryManager.TestResults.TestResultExistsAsync(request.UserId, request.TypeTest, cancellationToken))
        {
            _loggerManager.LogWarn($"Query:GetTestResultsQuery - User hadn't test results:{request.TypeTest} doesn't exist");
            
            throw new TestResultsNotFoundException(request.UserId, request.TypeTest);
        }
        
        var resultsFunc = async() => await _repositoryManager.TestResults.GetTestResultsAsync(request.UserId, request.TypeTest, cancellationToken);

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var results = await _cacheManager.GetOrSetCacheValue("results", resultsFunc);
        
        _loggerManager.LogInfo($"Query:GetTestResultsQuery - User with id:{request.UserId} and Test:{request.TypeTest} was finished");

        return results;
    }
}