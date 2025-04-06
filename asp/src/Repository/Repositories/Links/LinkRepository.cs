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

    public Task<bool> CheckIdExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CheckMaskExistsAsync(string mask, string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Links.AnyAsync(el => el.Mask == mask && el.UserId == userId, cancellationToken);
    }

    public async Task<Link> CreateAsync(Link entityRequest, CancellationToken cancellationToken = default)
    {
        await _context.Links.AddAsync(entityRequest, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entityRequest;
    }

    public Task<Link?> DeleteAsync(Link entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Link>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Link?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Link?> GetByMaskAsync(string mask, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Link?> UpdateAsync(Link entityStorage, Link entityRequest, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}