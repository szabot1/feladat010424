using System.ComponentModel.DataAnnotations;

namespace Feladat0104.Models;

public record Movie
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public string? Title { get; init; }

    [Required]
    public string? Description { get; init; }

    public required virtual IList<Actor> Actors { get; set; }
}

public record MovieDto
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public ICollection<ActorDto> Actors { get; init; } = new List<ActorDto>();
}

public record CreateMovieDto
{
    [Required]
    public string? Title { get; init; }

    [Required]
    public string? Description { get; init; }

    [Required]
    public ICollection<Guid>? ActorIds { get; init; }
}

public record UpdateMovieDto
{
    [Required]
    public string? Title { get; init; }

    [Required]
    public string? Description { get; init; }
}

public static class MovieExtensions
{
    public static MovieDto AsDto(this Movie movie)
    {
        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            Actors = movie.Actors.Select(p => new ActorDto
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList()
        };
    }
}