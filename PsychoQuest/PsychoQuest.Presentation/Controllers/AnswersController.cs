using Application.CommandsQueries.Answer.Commands.DeleteAnswers;
using Application.CommandsQueries.Answer.Commands.SaveAnswers;
using Application.CommandsQueries.Answer.Queries.GetAnswers;
using Entities.Enums;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsychoQuest.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class AnswersController : BaseController
{
    public AnswersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{typeTest:TypeTest}")]
    public async Task<IActionResult> GetAnswers(TypeTest typeTest)
    {
        var getAnswersQuery = new GetTestAnswersQuery()
        {
            UserId = UserId,
            TypeTest = typeTest
        };

        var answers = await Mediator.Send(getAnswersQuery);
        
        return Ok(answers);
    }

    [HttpPost("{typeTest:TypeTest}")]
    public async Task<IActionResult> SaveAnswers(TypeTest typeTest, [FromBody] TestAnswers answers)
    {
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

        await Mediator.Send(saveAnswersCommand);
        return RedirectToAction("GetTestResults","Results",new { typeTest = typeTest});
    }

    [HttpDelete("{typeTest:TypeTest}")]
    public async Task<IActionResult> DeleteAnswers(TypeTest typeTest)
    {
        var deleteAnswersCommand = new DeleteTestAnswersCommand()
        {
            UserId = UserId,
            TypeTest = typeTest
        };

        await Mediator.Send(deleteAnswersCommand);

        return Ok();
    }

}