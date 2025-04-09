using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface ITurnoRepository
{
    Task SaveChangesAsync();
    Task<Guid> AddAsync(Turno turno);
    Task<Guid> AddAndCommitAsync(Turno turno);
    Task<IEnumerable<Turno>> GetByBatalhaIdAsync(Guid batalhaId);
}