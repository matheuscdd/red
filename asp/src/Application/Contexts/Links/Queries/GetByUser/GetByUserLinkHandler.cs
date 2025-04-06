using Application.Contexts.Links.Dtos;
using Application.Contexts.Links.Repositories;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.Contexts.Links.Queries.GetByUser;

public class GetByIdLinkHandler: IRequestHandler<GetByUserLinkQuery, IReadOnlyCollection<LinkDto>>
{
    private readonly ILinkRepository _linkRepository;
    public GetByIdLinkHandler(ILinkRepository linkRepository)
    {
        _linkRepository = linkRepository;
    }

    public async Task<IReadOnlyCollection<LinkDto>> Handle(
        GetByUserLinkQuery request,
        CancellationToken cancellationToken
    )
    {
        var entities = await _linkRepository.GetByUserAsync(request.UserId, cancellationToken);
        var dtos = entities.Adapt<IReadOnlyCollection<LinkDto>>();
        return dtos;
    }
}
