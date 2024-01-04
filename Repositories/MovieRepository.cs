using Feladat0104.Data;
using Feladat0104.Models;
using Microsoft.EntityFrameworkCore;

namespace Feladat0104.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly FeladatDbContext _context;

    public MovieRepository(FeladatDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {
        return await _context.Movies
        .Include(m => m.Actors)
        .ToListAsync();
    }

    public async Task<Movie?> GetMovieAsync(Guid id)
    {
        return await _context.Movies.Include(m => m.Actors).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Movie>> GetMoviesByActorIdAsync(Guid id)
    {
        return await _context.Movies
            .Include(p => p.Actors)
            .Where(p => p.Actors.Any(p => p.Id == id))
            .ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetMoviesByActorNameContainsAsync(string name)
    {
        return await _context.Movies
            .Include(p => p.Actors)
            .Where(p => p.Actors.Any(p => p.Name!.Contains(name)))
            .ToListAsync();
    }

    public async Task<Movie> CreateMovieAsync(CreateMovieDto dto)
    {
        var movie = new Movie
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Actors = await _context.Actors.Where(p => dto.ActorIds!.Contains(p.Id)).ToListAsync()
        };

        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie?> UpdateMovieAsync(Guid id, UpdateMovieDto dto)
    {
        var movie = await GetMovieAsync(id);
        if (movie is null)
        {
            return movie;
        }

        _context.Movies.Entry(movie).CurrentValues.SetValues(dto);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie?> DeleteMovieAsync(Guid id)
    {
        var movie = await GetMovieAsync(id);
        if (movie is null)
        {
            return movie;
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return movie;
    }
}