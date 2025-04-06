using Domain.Entities;

namespace Application.Contexts.Links.Repositories;

public interface ILinkRepository
{
    Task<List<Link>> GetByUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<Link?> GetByIdAndUserAsync(Guid id, string? userId, CancellationToken cancellationToken = default);
    Task<Link?> GetByMaskAsync(string mask, CancellationToken cancellationToken = default);
    Task<Link> CreateAsync(Link entityRequest, CancellationToken cancellationToken = default);
    Task<Link?> UpdateAsync(Link entityStorage, Link entityRequest, CancellationToken cancellationToken = default);
    Task<Link?> DeleteAsync(Link entity, CancellationToken cancellationToken = default);
    Task<bool> CheckMaskExistsAsync(string mask, string userId, CancellationToken cancellationToken = default);
    Task<bool> CheckIdExistsAsync(Guid id, string userId, CancellationToken cancellationToken = default);
}