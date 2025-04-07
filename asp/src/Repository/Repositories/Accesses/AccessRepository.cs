using System.Net;
using Application.Contexts.Accesses.Repositories;
using Repository.Context;
using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories.Accesses;

public class AccessRepository: IAccessRepository
{
    private readonly ApplicationDbContext _context;

    public AccessRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckUniqueIpAsync(IPAddress ip, Guid linkId, CancellationToken cancellationToken = default)
    {
        var lastSevenDays = DateTime.UtcNow.Date.AddDays(-7);
        var repeatedAccess = await _context.Accesses
            .AnyAsync(el => el.LinkId == linkId && el.Ip == ip && el.CreatedAt > lastSevenDays, cancellationToken);
        return !repeatedAccess;
    }

    public async Task<Access> CreateAsync(Access entityRequest, CancellationToken cancellationToken = default)
    {
        await _context.Accesses.AddAsync(entityRequest, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entityRequest;
    }

    public async Task<List<Access>> GetByLinkAsync(Guid linkId, string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Accesses
            .Include(el => el.Link)
            .Where(el => el.Link!.UserId == userId && el.LinkId == linkId)
            .ToListAsync(cancellationToken);
    }
}