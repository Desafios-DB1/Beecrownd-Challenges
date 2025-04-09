using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface IPokemonRepository : IRepository<Pokemon>
{
    Task<Pokemon?> FindByIdWithAtaquesAsync(Guid id);
    Task<List<Pokemon>> FindAllWithAtaquesAsync();
}