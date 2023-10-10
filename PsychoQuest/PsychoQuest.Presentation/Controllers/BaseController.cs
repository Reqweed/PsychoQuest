using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;

namespace PsychoQuest.Presentation.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILoggerManager _loggerManager;
    
    protected BaseController(IMediator mediator, ILoggerManager loggerManager)
    {
        _mediator = mediator;
        _loggerManager = loggerManager;
    }

    protected IMediator Mediator => _mediator;
    protected ILoggerManager LoggerManager => _loggerManager;

    protected long UserId => User.Identity!.IsAuthenticated ? long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value) : 0;
}