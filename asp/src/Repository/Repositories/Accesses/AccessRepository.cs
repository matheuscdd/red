using System.Net;
using Application.Contexts.Accesses.Repositories;
using Repository.Context;
using Domain.Entities;

namespace Repository.Repositories.Accesses;

public class AccessRepository: IAccessRepository
{
    private readonly ApplicationDbContext _context;

    public AccessRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<bool> CheckUniqueIpAsync(IPAddress IP, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Access> CreateAsync(Access entityRequest, CancellationToken cancellationToken = default)
    {
        await _context.Accesses.AddAsync(entityRequest, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entityRequest;
    }

    public Task<List<Access?>> GetByLinkAsync(Guid linkId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}