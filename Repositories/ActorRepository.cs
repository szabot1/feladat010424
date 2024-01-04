using Feladat0104.Data;
using Feladat0104.Models;
using Microsoft.EntityFrameworkCore;

namespace Feladat0104.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly FeladatDbContext _context;

    public ActorRepository(FeladatDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Actor>> GetActorsAsync()
    {
        return await _context.Actors
        .Include(a => a.Movies)
        .ToListAsync();
    }

    public async Task<Actor?> GetActorAsync(Guid id)
    {
        return await _context.Actors.Include(a => a.Movies).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Actor> CreateActorAsync(CreateActorDto dto)
    {
        var actor = new Actor
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Movies = new List<Movie>()
        };

        await _context.Actors.AddAsync(actor);
        await _context.SaveChangesAsync();
        return actor;
    }

    public async Task<Actor?> UpdateActorAsync(Guid id, UpdateActorDto dto)
    {
        var actor = await GetActorAsync(id);
        if (actor is null)
        {
            return actor;
        }

        _context.Actors.Entry(actor).CurrentValues.SetValues(dto);
        await _context.SaveChangesAsync();
        return actor;
    }

    public async Task<Actor?> DeleteActorAsync(Guid id)
    {
        var actor = await GetActorAsync(id);
        if (actor is null)
        {
            return actor;
        }

        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();
        return actor;
    }
}