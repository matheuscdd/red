using Application.Contexts.Links.Dtos;
using MediatR;

namespace Application.Contexts.Links.Queries.GetByUser;

public class GetByUserLinkQuery: IRequest<IReadOnlyCollection<LinkDto>>
{
    public required string UserId { get; set; }
    public GetByUserLinkQuery(string userId)
    {
        UserId = userId;
    }

    public GetByUserLinkQuery() {}
}