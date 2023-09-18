using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PsychoQuest.Presentation.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private readonly IMediator _mediator;

    protected BaseController(IMediator mediator) => _mediator = mediator;

    protected IMediator Mediator => _mediator;

    protected long UserId => User.Identity!.IsAuthenticated ? long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value) : 0;
}