using Application.CommandsQueries.Result.Commands.DeleteTestResults;
using Application.CommandsQueries.Result.Queries.GetTestResults;
using Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;

namespace PsychoQuest.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ResultsController : BaseController
{
    public ResultsController(IMediator mediator, ILoggerManager loggerManager) : base(mediator, loggerManager)
    {
    }

    [HttpGet("{typeTest:TypeTest}")]
    public async Task<IActionResult> GetTestResults(TypeTest typeTest)
    {
        LoggerManager.LogInfo($"Controller:Results Action:GetTestResults - User with id:{UserId} has begun");

        var getTestResultsQuery = new GetTestResultsQuery()
        {
            UserId = UserId,
            TypeTest = typeTest
        };

        var results = await Mediator.Send(getTestResultsQuery);

        LoggerManager.LogInfo($"Controller:Results Action:GetTestResults - User with id:{UserId} was finished");

        return Ok(results);
    }

    [HttpDelete("{typeTest:TypeTest}")]
    public async Task<IActionResult> DeleteTestResults(TypeTest typeTest)
    {
        LoggerManager.LogInfo($"Controller:Results Action:DeleteTestResults - User with id:{UserId} has begun");

        var deleteTestResultsCommand = new DeleteTestResultsCommand()
        {
            UserId = UserId,
            TypeTest = typeTest
        };

        await Mediator.Send(deleteTestResultsCommand);

        LoggerManager.LogInfo($"Controller:Results Action:DeleteTestResults - User with id:{UserId} was finished");

        return Ok();
    }
}