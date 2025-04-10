using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;
public class PokemonRepository(PokemonsDbContext context) : IPokemonRepository
{
    public async Task SalvarAsync()
    {
        await context.SaveChangesAsync();
    }
    public async Task<Guid> AdicionarAsync(Pokemon pokemon)
    {
        await context.Pokemons.AddAsync(pokemon);
        return pokemon.PokemonId;
    }
    public async Task<Guid> AdicionarESalvarAsync(Pokemon pokemon)
    {
        await context.Pokemons.AddAsync(pokemon);
        await SalvarAsync(); 
        return pokemon.PokemonId;
    }
    public async Task<Pokemon?> ObterPorIdAsync(Guid id)
    {
       return await context.Pokemons
           .FirstOrDefaultAsync(p => p.PokemonId == id);
    }

    public async Task<Pokemon?> ObterPorIdComAtaquesAsync(Guid id)
    {
        return await context.Pokemons
            .Include(p => p.PokemonAtaques)
            .ThenInclude(pa => pa.Ataque)
            .FirstOrDefaultAsync(p => p.PokemonId == id);
    }
    
    public async Task<List<Pokemon>> ObterTodosAsync()
    {
        return await context.Pokemons.ToListAsync();
    }

    public async Task<List<Pokemon>> ObterTodosComAtaquesAsync()
    {
        return await context.Pokemons
            .Include(p => p.PokemonAtaques)
            .ThenInclude(pa => pa.Ataque)
            .ToListAsync();
    }
    
    public void Atualizar(Pokemon pokemon)
    {
        context.Pokemons.Update(pokemon);
    }
    
    public async Task<Pokemon> AtualizarESalvarAsync(Pokemon updatedPokemon)
    {
        context.Pokemons.Update(updatedPokemon);
        await SalvarAsync(); 
        return updatedPokemon;
    }
    public void Remover(Pokemon pokemon)
    {
        context.Pokemons.Remove(pokemon);
    }
    public async Task RemoverESalvarAsync(Pokemon pokemon)
    {
        context.Pokemons.Remove(pokemon);
        await SalvarAsync(); 
    }
}