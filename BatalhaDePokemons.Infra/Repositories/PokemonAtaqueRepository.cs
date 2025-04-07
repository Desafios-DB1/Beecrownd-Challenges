using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;

public class PokemonAtaqueRepository (PokemonsDbContext context) : IPokemonAtaqueRepository
{
    public async Task SaveChangesAsync()
    { 
        await context.SaveChangesAsync();
    }

    public async Task AddAsync(PokemonAtaque pa)
    {
        await context.PokemonAtaques.AddAsync(pa);
    }

    public async Task AddAndCommitAsync(PokemonAtaque pa)
    {
        await context.PokemonAtaques.AddAsync(pa);
        await SaveChangesAsync();
    }
    
    public async Task<List<Ataque>> GetAtaquesByPokemonIdAsync(Guid pokemonId)
    {
        return await context.PokemonAtaques
            .Where(pa => pa.PokemonId == pokemonId)
            .Select(pa => pa.Ataque)
            .ToListAsync();
    }
}