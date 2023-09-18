using Application.CommandsQueries.Question.Queries.GetTestQuestions;
using Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsychoQuest.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class QuestionsController : BaseController
{
    
    public QuestionsController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet("{typeTest:TypeTest}")]
    public async Task<IActionResult> GetTestQuestions(TypeTest typeTest)
    {
        var getTestQuestionsQuery = new GetTestQuestionsQuery()
        {
            TypeTest = typeTest
        };

        var questions = await Mediator.Send(getTestQuestionsQuery);

        return Ok(questions);
    }
}