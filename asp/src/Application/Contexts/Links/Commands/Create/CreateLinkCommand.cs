using Application.Contexts.Links.Dtos;
using MediatR;


namespace Application.Contexts.Links.Commands.Create;

public class CreateLinkCommand : IRequest<LinkDto>
{
    public string? Mask { get; set; }
    public Uri? Destination { get; set; }
    public string? UserId { get; set; }
}