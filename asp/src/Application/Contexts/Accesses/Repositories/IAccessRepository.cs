using System.Net;
using Domain.Entities;

namespace Application.Contexts.Accesses.Repositories;

public interface IAccessRepository
{
    Task<List<Access>> GetByLinkAsync(Guid linkId, string userId, CancellationToken cancellationToken = default);
    Task<bool> CheckUniqueIpAsync(IPAddress ip, Guid linkId, CancellationToken cancellationToken = default);
    Task<Access> CreateAsync(Access entityRequest, CancellationToken cancellationToken = default);
}