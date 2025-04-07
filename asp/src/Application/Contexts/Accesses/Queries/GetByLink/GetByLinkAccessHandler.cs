using Application.Contexts.Accesses.Dtos;
using Application.Contexts.Accesses.Repositories;
using Mapster;
using MediatR;

namespace Application.Contexts.Accesses.Queries.GetByLink;

class GetByIdLinkHandler: IRequestHandler<GetByLinkAccessQuery, IReadOnlyCollection<AccessDto>>
{
    private readonly IAccessRepository _accessRepository;
    public GetByIdLinkHandler(IAccessRepository accessRepository)
    {
        _accessRepository = accessRepository;
    }

    public async Task<IReadOnlyCollection<AccessDto>> Handle(
        GetByLinkAccessQuery request,
        CancellationToken cancellationToken
    )
    {
        var entities = await _accessRepository.GetByLinkAsync(request.LinkId, request.UserId, cancellationToken);
        var dtos = entities.Adapt<IReadOnlyCollection<AccessDto>>();
        return dtos;
    }
}
