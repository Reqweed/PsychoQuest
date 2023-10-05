using Application.CommandsQueries.Result.Commands.DeleteTestResults;
using Application.CommandsQueries.Result.Queries.GetTestResults;
using Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsychoQuest.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ResultsController : BaseController
{
    public ResultsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{typeTest:TypeTest}")]
    public async Task<IActionResult> GetTestResults(TypeTest typeTest)
    {
        var getTestResultsQuery = new GetTestResultsQuery()
        {
            UserId = UserId,
            TypeTest = typeTest
        };

        var results = await Mediator.Send(getTestResultsQuery);

        return Ok(results);
    }

    [HttpDelete("{typeTest:TypeTest}")]
    public async Task<IActionResult> DeleteTestResults(TypeTest typeTest)
    {
        var deleteTestResultsCommand = new DeleteTestResultsCommand()
        {
            UserId = UserId,
            TypeTest = typeTest
        };

        await Mediator.Send(deleteTestResultsCommand);

        return Ok();
    }
}