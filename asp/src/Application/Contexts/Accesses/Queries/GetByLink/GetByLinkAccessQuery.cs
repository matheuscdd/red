using Application.Contexts.Accesses.Dtos;
using MediatR;

namespace Application.Contexts.Accesses.Queries.GetByLink;

public class GetByLinkAccessQuery: IRequest<IReadOnlyCollection<AccessDto>>
{
    public required string UserId { get; set; }
    public required Guid LinkId { get; set; }

    public GetByLinkAccessQuery() {}
}