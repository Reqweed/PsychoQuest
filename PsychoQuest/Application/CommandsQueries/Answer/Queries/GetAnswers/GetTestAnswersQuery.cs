using Entities.Enums;
using Entities.Models;
using MediatR;

namespace Application.CommandsQueries.Answer.Queries.GetAnswers;

public class GetTestAnswersQuery : IRequest<TestAnswers>
{
    public long UserId { get; set; }
    public TypeTest TypeTest { get; set; }
}