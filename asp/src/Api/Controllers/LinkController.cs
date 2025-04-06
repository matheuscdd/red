using System.Security.Claims;
using Application.Contexts.Links.Commands.Create;
using Application.Contexts.Links.Queries.GetByUser;
using Application.Contexts.Links.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Contexts.Links.Commands.Update;
using Application.Contexts.Links.Commands.Delete;

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

    [HttpPost("private")]
    [Authorize]
    public async Task<IActionResult> CreatePrivate(
        [FromBody] CreateLinkCommand createLinkCommand
    )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        createLinkCommand.UserId = userId;
        var response = await _mediator.Send(createLinkCommand);
        _logger.LogInformation($"Link Created - UserId: {userId}");
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPost("public")]
    public async Task<IActionResult> CreatePublic(
        [FromBody] CreateLinkCommand createLinkCommand
    )
    {
        createLinkCommand.UserId = null;
        var response = await _mediator.Send(createLinkCommand);
        _logger.LogInformation($"Link Created - Public");
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("private/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var response = await _mediator.Send(new GetByIdLinkQuery{Id = id, UserId = userId});
        return Ok(response);
    }

    [HttpDelete("private/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _mediator.Send(new DeleteLinkCommand{Id = id, UserId = userId});
        return NoContent();
    }

    [HttpPut("private/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateLinkCommand updateLinkCommand
    )
    {
        updateLinkCommand.Id = id;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        updateLinkCommand.UserId = userId;
        var response = await _mediator.Send(updateLinkCommand);
        _logger.LogInformation($"Link {id} Updated - UserId: {userId}");
        return Ok(response);
    }

    [HttpGet("private")]
    [Authorize]
    public async Task<IActionResult> GetAllByUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var response = await _mediator.Send(new GetByUserLinkQuery{UserId = userId});
        return Ok(response);
    }
}