using Entities.Enums;
using Entities.Models;
using MediatR;

namespace Application.CommandsQueries.Question.Queries.GetTestQuestions;

public class GetTestQuestionsQuery : IRequest<TestQuestions>
{
    public TypeTest TypeTest { get; set; }
}