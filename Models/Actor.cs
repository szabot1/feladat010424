using System.ComponentModel.DataAnnotations;

namespace Feladat0104.Models;

public record Actor
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public string? Name { get; init; }

    public required virtual IList<Movie> Movies { get; set; }
}

public record ActorDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public ICollection<MovieDto> Movies { get; init; } = new List<MovieDto>();
}

public record CreateActorDto
{
    [Required]
    [MaxLength(20)]
    public string? Name { get; init; }
}

public record UpdateActorDto
{
    [Required]
    [MaxLength(20)]
    public string? Name { get; init; }
}

public static class ActorExtensions
{
    public static ActorDto AsDto(this Actor actor)
    {
        return new ActorDto
        {
            Id = actor.Id,
            Name = actor.Name,
            Movies = actor.Movies.Select(p => new MovieDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
            }).ToList()
        };
    }
}