using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;
public class PokemonRepository(PokemonsDbContext context) : IPokemonRepository
{
    public async Task AddAsync(Pokemon pokemon)
    {
        await context.Pokemons.AddAsync(pokemon);
        await context.SaveChangesAsync();
    }
    public async Task<Pokemon?> FindByIdAsync(Guid id)
    {
       return await context.Pokemons
           .FirstOrDefaultAsync(p => p.PokemonId == id);
    }

    public async Task<Pokemon?> FindByIdWithAtaquesAsync(Guid id)
    {
        return await context.Pokemons
            .Include(p => p.PokemonAtaques)
            .ThenInclude(pa => pa.Ataque)
            .FirstOrDefaultAsync(p => p.PokemonId == id);
    }
    
    public async Task<ICollection<Pokemon>> FindAllAsync()
    {
        return await context.Pokemons.ToListAsync();
    }
    
    public async Task UpdateAsync(Pokemon updatedPokemon)
    {
        context.Pokemons.Update(updatedPokemon);
        await context.SaveChangesAsync();
    }
    
    public async Task RemoveAsync(Pokemon pokemon)
    {
        context.Pokemons.Remove(pokemon);
        await context.SaveChangesAsync();
    }
}