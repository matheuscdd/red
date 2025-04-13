
using System.Net;
using System.Security.Claims;
using Application.Contexts.Accesses.Commands.Create;
using Application.Contexts.Accesses.Queries.GetByLink;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UAParser;

namespace Api.Controllers;

[ApiController]
[Route("api/accesses")]
public class AccessController : ControllerBase
{
    private readonly ILogger<AccessController> _logger;
    private readonly IMediator _mediator;

    public AccessController(ILogger<AccessController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("private/{userName}/{mask}")]
    public async Task<IActionResult> RedirectPrivate(
        [FromRoute] string userName,
        [FromRoute] string mask,
        [FromQuery] CreateAccessQueryParams createAccessQueryParams,
        [FromHeader(Name = "User-Agent")] string userAgent
    )
    {
        ClientInfo client = Parser.GetDefault().Parse(userAgent);
        var os = client.OS.Family;
        if (!client.OS.Major.IsNullOrEmpty())
        {
            os += $" {client.OS.Major}";
        }
        var browser = client.UA.Family;
        var ip = HttpContext.Connection.RemoteIpAddress;
        
        var response = await _mediator.Send(new CreateAccessCommand{
            Ip = ip!,
            Mask = mask, 
            UserName = userName,
            OS = os,
            Browser = browser,
            Origin = createAccessQueryParams.Origin
        });

        return Redirect(response.Destination.ToString());
    }

    [HttpGet("public/{mask}")]
    public async Task<IActionResult> RedirectPublic(
        [FromRoute] string mask,
        [FromQuery] CreateAccessQueryParams createAccessQueryParams,
        [FromHeader(Name = "User-Agent")] string userAgent
    )
    {
        ClientInfo client = Parser.GetDefault().Parse(userAgent);
        var os = client.OS.Family;
        if (!client.OS.Major.IsNullOrEmpty())
        {
            os += $" {client.OS.Major}";
        }
        var browser = client.UA.Family;
        var ip = HttpContext.Connection.RemoteIpAddress;
        
        var response = await _mediator.Send(new CreateAccessCommand{
            Ip = ip!,
            Mask = mask, 
            UserName = null,
            OS = os,
            Browser = browser,
            Origin = createAccessQueryParams.Origin
        });

        return Redirect(response.Destination.ToString());
    }

    [HttpGet("links/{linkId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetByLink(
        [FromRoute] Guid linkId
    )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var response = await _mediator.Send(new GetByLinkAccessQuery{LinkId = linkId, UserId = userId});
        return Ok(response);
    }
}