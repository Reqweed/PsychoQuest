using Entities.Exceptions.NotFoundException;
using Entities.Models;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.Result.Queries.GetTestResults;

public class GetTestResultsQueryHandler : IRequestHandler<GetTestResultsQuery,TestResults>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetTestResultsQueryHandler(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public async Task<TestResults> Handle(GetTestResultsQuery request, CancellationToken cancellationToken)
    {
        if(await _repositoryManager.TestResults.TestResultExistsAsync(request.UserId, request.TypeTest)) 
            throw new TestResultsNotFoundException(request.UserId, request.TypeTest);

        var results = await _repositoryManager.TestResults.GetTestResultsAsync(request.UserId, request.TypeTest);

        return results;
    }
}