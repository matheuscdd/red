using Application.Contexts.Links.Dtos;
using Application.Contexts.Links.Repositories;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.Contexts.Links.Queries.GetById;

public class GetByIdLinkHandler: IRequestHandler<GetByIdLinkQuery, LinkDto?>
{
    private readonly ILinkRepository _linkRepository;
    public GetByIdLinkHandler(ILinkRepository linkRepository)
    {
        _linkRepository = linkRepository;
    }

    public async Task<LinkDto?> Handle(
        GetByIdLinkQuery request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _linkRepository.GetByIdAndUserIdAsync(request.Id, request.UserId, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundCustomException("Link not found");
        }

        var dto = entity.Adapt<LinkDto>();
        return dto;
    }
}
