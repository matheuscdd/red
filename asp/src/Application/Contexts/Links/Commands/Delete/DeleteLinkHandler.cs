using Application.Contexts.Links.Dtos;
using Application.Contexts.Links.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Contexts.Links.Commands.Delete;

public class DeleteLinkHandler : IRequestHandler<DeleteLinkCommand>
{
    private readonly ILinkRepository _linkRepository;
    private readonly IMapper _mapper;

    public DeleteLinkHandler(ILinkRepository linkRepository
    )
    {
        _linkRepository = linkRepository;
    }

    public async Task Handle(
        DeleteLinkCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _linkRepository.GetByIdAndUserAsync(request.Id, request.UserId, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundCustomException("Link not found");
        }

        await _linkRepository.DeleteAsync(entity, cancellationToken);
    }
}