using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface IPokemonAtaqueRepository
{
    Task SaveChangesAsync();
    Task AddAsync(PokemonAtaque pa);
    Task AddAndCommitAsync(PokemonAtaque pa);
    Task<List<Ataque>> GetAtaquesByPokemonIdAsync(Guid pokemonId);
}