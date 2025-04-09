using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface IBatalhaRepository : IRepository<Batalha>
{
    Task<Batalha?> ObterPorIdComTurnosAsync(Guid batalhaId);
    Task<List<Batalha>> ObterTodosComTurnosAsync();
    Task<bool> ExisteBatalhaAtivaComPokemonAsync(Guid pokemon1Id, Guid pokemon2Id);
}