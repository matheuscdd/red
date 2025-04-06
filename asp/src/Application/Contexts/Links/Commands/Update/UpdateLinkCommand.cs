using Application.Contexts.Links.Dtos;
using MediatR;


namespace Application.Contexts.Links.Commands.Update;

public class UpdateLinkCommand : IRequest<LinkDto>
{
    public Guid Id { get; set; }
    public string? Mask { get; set; }
    public Uri? Destination { get; set; }
    public string? UserId { get; set; }
}