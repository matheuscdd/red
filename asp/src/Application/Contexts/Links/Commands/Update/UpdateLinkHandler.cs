using Application.Contexts.Links.Dtos;
using Application.Contexts.Links.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Links.Commands.Update;

public class UpdateLinkHandler : IRequestHandler<UpdateLinkCommand, LinkDto>
{
    private readonly ILinkRepository _linkRepository;
    private readonly IMapper _mapper;

    public UpdateLinkHandler(ILinkRepository linkRepository
    )
    {
        _linkRepository = linkRepository;
    }

    public async Task<LinkDto> Handle(
        UpdateLinkCommand request,
        CancellationToken cancellationToken
    )
    {
        if (request.RandomMask)
        {
            request.Mask = Guid.NewGuid().ToString()[..5];
        }

        var entityStorage = await _linkRepository.GetByIdAndUserIdAsync(request.Id, request.UserId, cancellationToken);
        if (entityStorage == null)
        {
            throw new NotFoundCustomException("Link not found");
        }

        var maskStorage = await _linkRepository.GetByMaskAndUserIdAsync(request.Mask!, request.UserId!, cancellationToken);
        if (maskStorage != null && maskStorage.Id != request.Id)
        {
            throw new ConflictCustomException("This mask already exists for another link");
        }

        var entityRequest = new Link(request.Mask, request.Destination, request.UserId);
        entityRequest = await _linkRepository.UpdateAsync(entityStorage, entityRequest, cancellationToken);
        var dto = entityRequest.Adapt<LinkDto>();
        return dto;
    }
}