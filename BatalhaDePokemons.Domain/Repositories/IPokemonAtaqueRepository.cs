using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface IPokemonAtaqueRepository
{
    Task SalvarAsync();
    Task AdicionarAsync(PokemonAtaque pa);
    Task AdicionarESalvarAsync(PokemonAtaque pa);
    Task<List<Ataque>> ObterAtaquesPorPokemonIdAsync(Guid pokemonId);
}