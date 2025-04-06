using Application.Contexts.Links.Dtos;
using MediatR;

namespace Application.Contexts.Links.Queries.GetById;

public class GetByIdLinkQuery: IRequest<LinkDto?>
{
    public required Guid Id { get; set; }
    public required string? UserId { get; set; }
    public GetByIdLinkQuery(Guid id, string? userId)
    {
        Id = id;
        UserId = userId;
    }

    public GetByIdLinkQuery() {}
}