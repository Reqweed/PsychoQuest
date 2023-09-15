using Entities.Exceptions.NotFoundException;
using Entities.Models;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.Question.Queries.GetTestQuestions;

public class GetTestQuestionsQueryHandler : IRequestHandler<GetTestQuestionsQuery,TestQuestions>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetTestQuestionsQueryHandler(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public async Task<TestQuestions> Handle(GetTestQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = await _repositoryManager.TestQuestions.GetTestQuestionsAsync(request.TypeTest);

        if (questions is null) 
            throw new TestQuestionsNotFoundException(request.TypeTest);

        return questions;
    }
}