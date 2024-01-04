using Microsoft.AspNetCore.Mvc;
using Feladat0104.Models;
using Feladat0104.Repositories;

namespace Feladat0104.Controllers;

[ApiController]
[Route("api/actors")]
public class ActorController : ControllerBase
{
    private readonly IActorRepository _repository;
    private readonly IMovieRepository _movieRepository;

    public ActorController(IActorRepository repository, IMovieRepository movieRepository)
    {
        _repository = repository;
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<ActorDto>> GetAll()
    {
        return (await _repository.GetActorsAsync())
            .Select(actor => actor.AsDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActorDto>> GetById(Guid id)
    {
        var actor = await _repository.GetActorAsync(id);

        if (actor is null)
        {
            return NotFound();
        }

        return actor.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<ActorDto>> Create(CreateActorDto actorDto)
    {
        var actor = await _repository.CreateActorAsync(actorDto);

        return CreatedAtAction(nameof(GetById), new { id = actor.Id }, actor.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ActorDto>> Update(Guid id, UpdateActorDto actorDto)
    {
        var actor = await _repository.UpdateActorAsync(id, actorDto);

        if (actor is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ActorDto>> Delete(Guid id)
    {
        var actor = await _repository.DeleteActorAsync(id);

        if (actor is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}/movies")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesByActorId(Guid id)
    {
        var actor = await _repository.GetActorAsync(id);

        if (actor is null)
        {
            return NotFound();
        }

        var movies = await _movieRepository.GetMoviesByActorIdAsync(id);

        return Ok(movies.Select(p => p.AsDto()));
    }
}