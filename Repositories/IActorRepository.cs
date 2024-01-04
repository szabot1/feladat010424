using Feladat0104.Models;

namespace Feladat0104.Repositories;

public interface IActorRepository
{
    Task<IEnumerable<Actor>> GetActorsAsync();

    Task<Actor?> GetActorAsync(Guid id);

    Task<Actor> CreateActorAsync(CreateActorDto dto);

    Task<Actor?> UpdateActorAsync(Guid id, UpdateActorDto dto);

    Task<Actor?> DeleteActorAsync(Guid id);
}