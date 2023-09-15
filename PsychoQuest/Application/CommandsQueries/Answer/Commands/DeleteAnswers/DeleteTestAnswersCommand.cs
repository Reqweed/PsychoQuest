using Entities.Enums;
using MediatR;

namespace Application.CommandsQueries.Answer.Commands.DeleteAnswers;

public class DeleteTestAnswersCommand : IRequest
{
    public long UserId { get; set; }
    public TypeTest TypeTest { get; set; }
}