using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;

public class PokemonAtaqueRepository (PokemonsDbContext context) : IPokemonAtaqueRepository
{
    public async Task AddAsync(PokemonAtaque entity)
    {
        await context.PokemonAtaques.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<ICollection<Ataque>> GetAtaquesByPokemonIdAsync(Guid pokemonId)
    {
        return await context.PokemonAtaques
            .Where(pa => pa.PokemonId == pokemonId)
            .Select(pa => pa.Ataque)
            .ToListAsync();
    }
}