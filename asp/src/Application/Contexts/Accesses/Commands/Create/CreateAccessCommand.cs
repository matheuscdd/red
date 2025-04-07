using System.Net;
using Application.Contexts.Links.Dtos;
using MediatR;


namespace Application.Contexts.Accesses.Commands.Create;

public class CreateAccessCommand : IRequest<LinkDto>
{
    public required string Mask { get; set; }
    public IPAddress Ip { get; set; } = IPAddress.None;
    public string? UserName { get; set; }
    public string? OS { get; set; }
    public string? Browser { get; set; }
    public string? Origin { get; set; }
}