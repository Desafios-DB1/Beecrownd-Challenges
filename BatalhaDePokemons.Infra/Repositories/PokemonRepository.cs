using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;
public class PokemonRepository(PokemonsDbContext context) : IPokemonRepository
{
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    public async Task<Guid> AddAsync(Pokemon pokemon)
    {
        await context.Pokemons.AddAsync(pokemon);
        return pokemon.PokemonId;
    }
    public async Task<Guid> AddAndCommitAsync(Pokemon pokemon)
    {
        await context.Pokemons.AddAsync(pokemon);
        await SaveChangesAsync(); 
        return pokemon.PokemonId;
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
    
    public async Task<List<Pokemon>> FindAllAsync()
    {
        return await context.Pokemons.ToListAsync();
    }

    public async Task<List<Pokemon>> FindAllWithAtaquesAsync()
    {
        return await context.Pokemons
            .Include(p => p.PokemonAtaques)
            .ThenInclude(pa => pa.Ataque)
            .ToListAsync();
    }
    
    public void Update(Pokemon pokemon)
    {
        context.Pokemons.Update(pokemon);
    }
    
    public async Task<Pokemon> UpdateAndCommitAsync(Pokemon updatedPokemon)
    {
        context.Pokemons.Update(updatedPokemon);
        await SaveChangesAsync(); 
        return updatedPokemon;
    }
    public void Remove(Pokemon pokemon)
    {
        context.Pokemons.Remove(pokemon);
    }
    public async Task RemoveAndCommitAsync(Pokemon pokemon)
    {
        context.Pokemons.Remove(pokemon);
        await SaveChangesAsync(); 
    }
}