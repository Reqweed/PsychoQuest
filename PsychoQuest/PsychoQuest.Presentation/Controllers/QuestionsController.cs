using Application.CommandsQueries.Question.Queries.GetTestQuestions;
using Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PsychoQuest.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuestionsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{typeTest:TypeTest}")]
    public async Task<IActionResult> GetTestQuestions(TypeTest typeTest)
    {
        var getTestQuestionsQuery = new GetTestQuestionsQuery()
        {
            TypeTest = typeTest
        };

        var questions = await _mediator.Send(getTestQuestionsQuery);

        return Ok(questions);
    }
}