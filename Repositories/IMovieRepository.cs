using Feladat0104.Models;

namespace Feladat0104.Repositories;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetMoviesAsync();

    Task<Movie?> GetMovieAsync(Guid id);

    Task<IEnumerable<Movie>> GetMoviesByActorIdAsync(Guid id);

    Task<IEnumerable<Movie>> GetMoviesByActorNameContainsAsync(string name);

    Task<Movie> CreateMovieAsync(CreateMovieDto dto);

    Task<Movie?> UpdateMovieAsync(Guid id, UpdateMovieDto dto);

    Task<Movie?> DeleteMovieAsync(Guid id);
}