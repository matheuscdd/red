using Domain.Entities;
using Repository.Context;
using Microsoft.EntityFrameworkCore;
using Application.Contexts.Links.Repositories;

namespace Repository.Repositories.Links;

public class LinkRepository: ILinkRepository
{
    private readonly ApplicationDbContext _context;

    public LinkRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckIdExistsAsync(Guid id, string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Links
            .AnyAsync(el => el.Id == id && el.UserId == userId, cancellationToken);
    }

    public async Task<bool> CheckMaskExistsAsync(string mask, string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Links
            .AnyAsync(el => el.Mask == mask && el.UserId == userId, cancellationToken);
    }

    public async Task<Link> CreateAsync(Link entityRequest, CancellationToken cancellationToken = default)
    {
        await _context.Links.AddAsync(entityRequest, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entityRequest;
    }

    public async Task<Link?> DeleteAsync(Link entity, CancellationToken cancellationToken = default)
    {
        _context.Links.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<List<Link>> GetByUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Links
            .Include(el => el.User)
            .Where(el => el.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Link?> GetByIdAndUserAsync(Guid id, string? userId, CancellationToken cancellationToken = default)
    {
        return await _context.Links
            .Include(el => el.User)
            .FirstOrDefaultAsync(el => el.Id == id && el.UserId == userId, cancellationToken);
    }

    public Task<Link?> GetByMaskAsync(string mask, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Link?> UpdateAsync(Link entityStorage, Link entityRequest, CancellationToken cancellationToken = default)
    {
        entityStorage.SetDestination(entityRequest.Destination);
        entityStorage.SetMask(entityRequest.Mask);

        await _context.SaveChangesAsync(cancellationToken);

        return entityStorage;
    }
}