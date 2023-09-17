using Application.CommandsQueries.Result.Commands.DeleteTestResults;
using Application.CommandsQueries.Result.Queries.GetTestResults;
using Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PsychoQuest.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResultsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{typeTest:TypeTest}")]
    public async Task<IActionResult> GetTestResults(TypeTest typeTest)
    {
        var getTestResultsQuery = new GetTestResultsQuery()
        {
            UserId = 1, //fix user
            TypeTest = typeTest
        };

        var results = await _mediator.Send(getTestResultsQuery);

        return Ok(results);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTestResults(TypeTest typeTest)
    {
        var deleteTestResultsCommand = new DeleteTestResultsCommand()
        {
            UserId = 1, //fix user
            TypeTest = typeTest
        };

        await _mediator.Send(deleteTestResultsCommand);

        return Ok();
    }
}