using Entities.Enums;
using Entities.Models;
using MediatR;

namespace Application.CommandsQueries.Result.Queries.GetTestResults;

public class GetTestResultsQuery : IRequest<TestResults>
{
    public long UserId { get; set; }
    public TypeTest TypeTest { get; set; }
}