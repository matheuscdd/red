using Application.Contexts.Links.Dtos;
using Application.Contexts.Links.Repositories;
using Application.Contexts.Links.Commands.Create;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Links.Commands.Create;

public class CreateLinkHandler : IRequestHandler<CreateLinkCommand, LinkDto>
{
    private readonly ILinkRepository _linkRepository;
    private readonly IMapper _mapper;

    public CreateLinkHandler(
        ILinkRepository linkRepository
    )
    {
        _linkRepository = linkRepository;
    }

    public async Task<LinkDto> Handle(
        CreateLinkCommand request,
        CancellationToken cancellationToken
    )
    {
        if (request.RandomMask)
        {
            request.Mask = Guid.NewGuid().ToString()[..5];
        }
        else
        {
            var maskExists = await _linkRepository.CheckMaskExistsAsync(request.Mask, request.UserId, cancellationToken);
            if (maskExists)
            {
                throw new ConflictCustomException("This mask already exists");
            }
        }

        var entity = new Link(request.Mask, request.Destination, request.UserId);
        entity = await _linkRepository.CreateAsync(entity, cancellationToken);
        entity = await _linkRepository.GetByIdAndUserIdAsync(entity.Id, entity.UserId, cancellationToken);
        var dto = entity.Adapt<LinkDto>();
        return dto;
    }
}