using Entities.Exceptions.NotFoundException;
using Entities.Models;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.Answer.Queries.GetAnswers;

public class GetTestAnswersQueryHandler : IRequestHandler<GetTestAnswersQuery,TestAnswers>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetTestAnswersQueryHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<TestAnswers> Handle(GetTestAnswersQuery request, CancellationToken cancellationToken)
    {
        if (await _repositoryManager.TestAnswers.TestAnswersExistsAsync(request.UserId, request.TypeTest)) 
            throw new TestAnswersNotFoundException(request.UserId, request.TypeTest);

        var answers = await _repositoryManager.TestAnswers.GetTestAnswersAsync(request.UserId, request.TypeTest);

        return answers;
    }
}