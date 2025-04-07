using Application.Contexts.Accesses.Repositories;
using Application.Contexts.Links.Dtos;
using Application.Contexts.Links.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Accesses.Commands.Create;

public class CreateAccessHandler : IRequestHandler<CreateAccessCommand, LinkDto>
{
    private readonly ILinkRepository _linkRepository;
    private readonly IAccessRepository _accessRepository;
    private readonly IMapper _mapper;

    public CreateAccessHandler(
        ILinkRepository linkRepository,
        IAccessRepository accessRepository
    )
    {
        _linkRepository = linkRepository;
        _accessRepository = accessRepository;
    }

    public async Task<LinkDto> Handle(
        CreateAccessCommand request,
        CancellationToken cancellationToken
    )
    {
        var linkEntity = await _linkRepository.GetByMaskAndUserNameAsync(request.Mask!, request.UserName, cancellationToken);
        if (linkEntity == null)
        {
            throw new NotFoundCustomException("Link not found");
        }
        var linkDto = linkEntity.Adapt<LinkDto>();

        // var uniqueAccess = await _accessRepository.CheckUniqueIpAsync(request.Ip);
        // if (!uniqueAccess)
        // {
        //     return linkDto;
        // }

        await _accessRepository.CreateAsync(new Access(
            request.Ip,
            "nada",
            "lugar",
            "nenhum",
            request.Browser!,
            request.OS!,
            request.Origin,
            linkEntity.Id
        ), cancellationToken);

        return linkDto;
    }
}