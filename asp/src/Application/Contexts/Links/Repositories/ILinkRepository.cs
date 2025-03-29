using Domain.Entities;

namespace Application.Contexts.Links.Repositories;

public interface ILinkRepository
{
    Task<List<Link>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Link?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Link?> GetByMaskAsync(string mask, CancellationToken cancellationToken = default);
    Task<Link> CreateAsync(Link entityRequest, CancellationToken cancellationToken = default);
    Task<Link?> UpdateAsync(Link entityStorage, Link entityRequest, CancellationToken cancellationToken = default);
    Task<Link?> DeleteAsync(Link entity, CancellationToken cancellationToken = default);
    Task<bool> CheckIdExistsAsync(Guid id, CancellationToken cancellationToken = default);
}