using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;

public class BatalhaRepository(PokemonsDbContext context) : IBatalhaRepository
{
    public async Task SalvarAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<Guid> AdicionarAsync(Batalha batalha)
    {
        await context.Batalhas.AddAsync(batalha);
        return batalha.BatalhaId;
    }

    public async Task<Guid> AdicionarESalvarAsync(Batalha batalha)
    {
        await context.Batalhas.AddAsync(batalha);
        await SalvarAsync();
        return batalha.BatalhaId;
    }

    public async Task<Batalha?> ObterPorIdAsync(Guid id)
    {
        return await context.Batalhas
            .FirstOrDefaultAsync(b => b.BatalhaId == id);
    }
    
    public async Task<Batalha?> ObterPorIdComTurnosAsync(Guid batalhaId)
    {
        return await context.Batalhas
            .Include(b => b.Turnos)
            .FirstOrDefaultAsync(b => b.BatalhaId == batalhaId);
    }

    public async Task<List<Batalha>> ObterTodosAsync()
    {
        return await context.Batalhas.ToListAsync();
    }

    public async Task<List<Batalha>> ObterTodosComTurnosAsync()
    {
        return await context.Batalhas
            .Include(b => b.Turnos)
            .ToListAsync();
    }

    public void Atualizar(Batalha batalha)
    {
        context.Batalhas.Update(batalha);
    }

    public async Task<Batalha> AtualizarESalvarAsync(Batalha updatedBatalha)
    {
        context.Batalhas.Update(updatedBatalha);
        await SalvarAsync();
        return updatedBatalha;
    }

    public void Remover(Batalha batalha)
    {
        context.Batalhas.Remove(batalha);
    }

    public async Task RemoverESalvarAsync(Batalha batalha)
    {
        context.Batalhas.Remove(batalha);
        await SalvarAsync();
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