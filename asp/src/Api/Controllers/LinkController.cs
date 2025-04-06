using System.Security.Claims;
using Application.Contexts.Links.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/links")]
public class LinkController: ControllerBase
{
    private readonly ILogger<LinkController> _logger;
    private readonly IMediator _mediator;

    public LinkController(ILogger<LinkController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(
        [FromBody] CreateLinkCommand createLinkCommand
    )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        createLinkCommand.UserId = userId;
        var response = await _mediator.Send(createLinkCommand);
        _logger.LogInformation($"Link Created - UserId: {userId}");
        return Ok(); // TODO - trocar quando tiver create
    }
}