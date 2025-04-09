using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;

public class BatalhaRepository(PokemonsDbContext context) : IBatalhaRepository
{
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<Guid> AddAsync(Batalha batalha)
    {
        await context.Batalhas.AddAsync(batalha);
        return batalha.BatalhaId;
    }

    public async Task<Guid> AddAndCommitAsync(Batalha batalha)
    {
        await context.Batalhas.AddAsync(batalha);
        await SaveChangesAsync();
        return batalha.BatalhaId;
    }

    public async Task<Batalha?> FindByIdAsync(Guid id)
    {
        return await context.Batalhas
            .FirstOrDefaultAsync(b => b.BatalhaId == id);
    }
    
    public async Task<Batalha?> FindByIdWithTurnos(Guid batalhaId)
    {
        return await context.Batalhas
            .Include(b => b.Turnos)
            .FirstOrDefaultAsync(b => b.BatalhaId == batalhaId);
    }

    public async Task<List<Batalha>> FindAllAsync()
    {
        return await context.Batalhas.ToListAsync();
    }

    public async Task<List<Batalha>> FindAllWithTurnosAsync()
    {
        return await context.Batalhas
            .Include(b => b.Turnos)
            .ToListAsync();
    }

    public void Update(Batalha batalha)
    {
        context.Batalhas.Update(batalha);
    }

    public async Task<Batalha> UpdateAndCommitAsync(Batalha updatedBatalha)
    {
        context.Batalhas.Update(updatedBatalha);
        await SaveChangesAsync();
        return updatedBatalha;
    }

    public void Remove(Batalha batalha)
    {
        context.Batalhas.Remove(batalha);
    }

    public async Task RemoveAndCommitAsync(Batalha batalha)
    {
        context.Batalhas.Remove(batalha);
        await SaveChangesAsync();
    }

    public async Task<bool> ExisteBatalhaAtivaComPokemonAsync(Guid pokemon1Id, Guid pokemon2Id)
    {
        return await context.Batalhas.AnyAsync(b => 
            !b.IsFinalizada &&
            (b.Pokemon1Id == pokemon1Id || b.Pokemon2Id == pokemon2Id ||
            b.Pokemon1Id == pokemon2Id || b.Pokemon2Id == pokemon1Id)
            );
    }
}