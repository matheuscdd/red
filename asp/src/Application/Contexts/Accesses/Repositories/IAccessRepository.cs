using System.Net;
using Domain.Entities;

namespace Application.Contexts.Accesses.Repositories;

public interface IAccessRepository
{
    Task<List<Access?>> GetByLinkAsync(Guid linkId, CancellationToken cancellationToken = default);
    Task<bool> CheckUniqueAsync(IPAddress IP, CancellationToken cancellationToken = default);
    Task<Access> CreateAsync(Access entityRequest, CancellationToken cancellationToken = default);
}