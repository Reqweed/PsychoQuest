using Application.CommandsQueries.Answer.Commands.DeleteAnswers;
using Application.CommandsQueries.Answer.Commands.SaveAnswers;
using Application.CommandsQueries.Answer.Queries.GetAnswers;
using Entities.Enums;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;

namespace PsychoQuest.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class AnswersController : BaseController
{
    public AnswersController(IMediator mediator, ILoggerManager loggerManager) : base(mediator, loggerManager)
    {
    }

    [HttpGet("{typeTest:TypeTest}")]
    public async Task<IActionResult> GetAnswers(TypeTest typeTest, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:Answers Action:GetAnswers - User with id:{UserId} has begun");
        
        var getAnswersQuery = new GetTestAnswersQuery()
        {
            UserId = UserId,
            TypeTest = typeTest
        };

        var answers = await Mediator.Send(getAnswersQuery, cancellationToken);
        
        LoggerManager.LogInfo($"Controller:Answers Action:GetAnswers - User with id:{UserId} was finished");
        
        return Ok(answers);
    }
    
    [HttpPost("{typeTest:TypeTest}")]
    public async Task<IActionResult> SaveAnswers(TypeTest typeTest, [FromBody] TestAnswers answers, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:Answers Action:SaveAnswers - User with id:{UserId} has begun");
        
        var saveAnswersCommand = new SaveTestAnswersCommand()
        {
            TestAnswers = new TestAnswers()
            {
                UserId = UserId,
                TestAnswersId = answers.TestAnswersId,
                Answers = answers.Answers,
                TestName = typeTest
            }
        };

        await Mediator.Send(saveAnswersCommand, cancellationToken);
        
        LoggerManager.LogInfo($"Controller:Answers Action:SaveAnswers - User with id:{UserId} was finished");
        
        return RedirectToAction("GetTestResults","Results",new { typeTest = typeTest});
    }

    [HttpDelete("{typeTest:TypeTest}")]
    public async Task<IActionResult> DeleteAnswers(TypeTest typeTest, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:Answers Action:DeleteAnswers - User with id:{UserId} has begun");
        
        var deleteAnswersCommand = new DeleteTestAnswersCommand()
        {
            UserId = UserId,
            TypeTest = typeTest
        };

        await Mediator.Send(deleteAnswersCommand, cancellationToken);

        LoggerManager.LogInfo($"Controller:Answers Action:DeleteAnswers - User with id:{UserId} was finished");
        
        return Ok();
    }

}