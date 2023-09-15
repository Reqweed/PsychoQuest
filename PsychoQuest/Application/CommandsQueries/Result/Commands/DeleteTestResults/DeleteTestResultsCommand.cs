using Entities.Enums;
using MediatR;

namespace Application.CommandsQueries.Result.Commands.DeleteTestResults;

public class DeleteTestResultsCommand : IRequest
{
    public long UserId { get; set; }
    public TypeTest TypeTest { get; set; }
}