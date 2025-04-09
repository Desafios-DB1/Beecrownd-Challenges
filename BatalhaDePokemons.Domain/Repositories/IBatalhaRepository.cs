using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface IBatalhaRepository : IRepository<Batalha>
{
    Task<Batalha?> FindByIdWithTurnos(Guid batalhaId);
    Task<List<Batalha>> FindAllWithTurnosAsync();
    Task<bool> ExisteBatalhaAtivaComPokemonAsync(Guid pokemon1Id, Guid pokemon2Id);
}