using Entities.Exceptions.NotFoundException;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.Result.Commands.DeleteTestResults;

public class DeleteTestResultsCommandHandler : IRequestHandler<DeleteTestResultsCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public DeleteTestResultsCommandHandler(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public async Task Handle(DeleteTestResultsCommand request, CancellationToken cancellationToken)
    {
        if (await _repositoryManager.TestResults.TestResultExistsAsync(request.UserId, request.TypeTest))
            throw new TestResultsNotFoundException(request.UserId, request.TypeTest);
        
        await _repositoryManager.TestResults.DeleteTestResultsAsync(request.UserId, request.TypeTest);
    }
}