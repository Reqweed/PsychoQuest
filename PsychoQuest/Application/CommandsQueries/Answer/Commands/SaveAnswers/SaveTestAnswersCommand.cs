using Entities.Models;
using MediatR;

namespace Application.CommandsQueries.Answer.Commands.SaveAnswers;

public class SaveTestAnswersCommand : IRequest
{
    public TestAnswers TestAnswers { get; set; }
}