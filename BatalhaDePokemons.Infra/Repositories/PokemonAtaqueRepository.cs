using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;

public class PokemonAtaqueRepository (PokemonsDbContext context) : IPokemonAtaqueRepository
{
    public async Task SalvarAsync()
    { 
        await context.SaveChangesAsync();
    }

    public async Task AdicionarAsync(PokemonAtaque pa)
    {
        await context.PokemonAtaques.AddAsync(pa);
    }

    public async Task AdicionarESalvarAsync(PokemonAtaque pa)
    {
        await context.PokemonAtaques.AddAsync(pa);
        await SalvarAsync();
    }
    
    public async Task<List<Ataque>> ObterAtaquesPorPokemonIdAsync(Guid pokemonId)
    {
        return await context.PokemonAtaques
            .Where(pa => pa.PokemonId == pokemonId)
            .Select(pa => pa.Ataque)
            .ToListAsync();
    }
}