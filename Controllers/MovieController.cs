using Microsoft.AspNetCore.Mvc;
using Feladat0104.Models;
using Feladat0104.Repositories;

namespace Feladat0104.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController : ControllerBase
{
    private readonly IMovieRepository _repository;

    public MovieController(IMovieRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<MovieDto>> GetAll()
    {
        return (await _repository.GetMoviesAsync())
            .Select(s => s.AsDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetById(Guid id)
    {
        var user = await _repository.GetMovieAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<MovieDto>> Create(CreateMovieDto movieDto)
    {
        var movie = await _repository.CreateMovieAsync(movieDto);

        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MovieDto>> Update(Guid id, UpdateMovieDto movieDto)
    {
        var movie = await _repository.UpdateMovieAsync(id, movieDto);

        if (movie is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MovieDto>> Delete(Guid id)
    {
        var movie = await _repository.DeleteMovieAsync(id);

        if (movie is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("by-actor-name/{name}")]
    public async Task<IEnumerable<MovieDto>> GetByActorName(string name)
    {
        return (await _repository.GetMoviesByActorNameContainsAsync(name))
            .Select(s => s.AsDto());
    }
}