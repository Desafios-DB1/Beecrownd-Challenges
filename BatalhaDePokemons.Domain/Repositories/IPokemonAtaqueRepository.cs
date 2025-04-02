using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface IPokemonAtaqueRepository
{
    public Task<ICollection<Ataque>> GetAtaquesByPokemonIdAsync(Guid pokemonId);
    public Task AddAsync(PokemonAtaque entity);
}