
using System.Net;
using Application.Contexts.Accesses.Commands.Create;
using MediatR;
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
}