using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface ITurnoRepository
{
    Task SalvarAsync();
    Task<Guid> AdicionarAsync(Turno turno);
    Task<Guid> AdicionarESalvarAsync(Turno turno);
    Task<IEnumerable<Turno>> ObterPorIdDaBatalhaAsync(Guid batalhaId);
}