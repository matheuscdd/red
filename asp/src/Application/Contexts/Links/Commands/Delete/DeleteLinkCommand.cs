using Application.Contexts.Links.Dtos;
using MediatR;


namespace Application.Contexts.Links.Commands.Delete;

public class DeleteLinkCommand : IRequest
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
}