using Entities.Exceptions.NotFoundException;
using MediatR;
using Repository.Contracts;

namespace Application.CommandsQueries.Answer.Commands.DeleteAnswers;

public class DeleteTestAnswersCommandHandler : IRequestHandler<DeleteTestAnswersCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public DeleteTestAnswersCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task Handle(DeleteTestAnswersCommand request, CancellationToken cancellationToken)
    {
        if (await _repositoryManager.TestAnswers.TestAnswersExistsAsync(request.UserId, request.TypeTest))
            throw new TestAnswersNotFoundException(request.UserId, request.TypeTest);

        await _repositoryManager.TestAnswers.DeleteTestAnswersAsync(request.UserId, request.TypeTest);
    }
}