using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface IPokemonRepository : IRepository<Pokemon>
{
    Task<Pokemon?> ObterPorIdComAtaquesAsync(Guid id);
    Task<List<Pokemon>> ObterTodosComAtaquesAsync();
}