﻿using Application.CommandsQueries.Question.Queries.GetTestQuestions;
using Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;

namespace PsychoQuest.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class QuestionsController : BaseController
{
    
    public QuestionsController(IMediator mediator, ILoggerManager loggerManager) : base(mediator, loggerManager)
    {
    }
    
    [ResponseCache(CacheProfileName = "Cache")]
    [HttpGet("{typeTest:TypeTest}")]
    public async Task<IActionResult> GetTestQuestions(TypeTest typeTest, CancellationToken cancellationToken)
    {
        LoggerManager.LogInfo($"Controller:Questions Action:GetTestQuestions - User with id:{UserId} has begun");

        var getTestQuestionsQuery = new GetTestQuestionsQuery()
        {
            TypeTest = typeTest
        };

        var questions = await Mediator.Send(getTestQuestionsQuery, cancellationToken);

        LoggerManager.LogInfo($"Controller:Questions Action:GetTestQuestions - User with id:{UserId} was finished");

        return Ok(questions);
    }
}