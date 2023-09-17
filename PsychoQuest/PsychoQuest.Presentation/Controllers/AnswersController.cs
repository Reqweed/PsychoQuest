using Application.CommandsQueries.Answer.Commands.DeleteAnswers;
using Application.CommandsQueries.Answer.Commands.SaveAnswers;
using Application.CommandsQueries.Answer.Queries.GetAnswers;
using Entities.Enums;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PsychoQuest.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnswersController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnswersController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{typeTest:TypeTest}")]
    public async Task<IActionResult> GetAnswers(TypeTest typeTest)
    {
        var getAnswersQuery = new GetTestAnswersQuery()
        {
            UserId = 1,//fix user
            TypeTest = typeTest
        };

        var answers = await _mediator.Send(getAnswersQuery);
        
        return Ok(answers);
    }

    [HttpPost("{typeTest:TypeTest}")]
    public async Task<IActionResult> SaveAnswers(TypeTest typeTest, [FromBody] TestAnswers answers)
    {
        answers.UserId = 1;//fix user
        answers.TestName = typeTest;
        
        var saveAnswersCommand = new SaveTestAnswersCommand()
        {
            TestAnswers = answers
        };

        await _mediator.Send(saveAnswersCommand);
        return RedirectToAction("GetTestResults","Results",new { typeTest = typeTest});
    }

    [HttpDelete("{typeTest:TypeTest}")]
    public async Task<IActionResult> DeleteAnswers(TypeTest typeTest)
    {
        var deleteAnswersCommand = new DeleteTestAnswersCommand()
        {
            UserId = 1,//fix user
            TypeTest = typeTest
        };

        await _mediator.Send(deleteAnswersCommand);

        return Ok();
    }

}