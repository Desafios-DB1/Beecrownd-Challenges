using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;

public class AtaqueRepository(PokemonsDbContext context) : IAtaqueRepository
{
    public async Task SaveChangesAsync()
    {
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Erro ao salvar as alterações no AtaqueRepository", e);
        }
    }

    public async Task<Guid> AddAsync(Ataque ataque)
    {
        await context.Ataques.AddAsync(ataque);
        return ataque.AtaqueId;
    }
    public async Task<Guid> AddAndCommitAsync(Ataque ataque)
    {
        await context.Ataques.AddAsync(ataque);
        await SaveChangesAsync(); 
        return ataque.AtaqueId;
    }
    public async Task<Ataque?> FindByIdAsync(Guid id)
    {
        return await context.Ataques
            .FirstOrDefaultAsync(p => p.AtaqueId == id);
    }
    
    public async Task<List<Ataque>> FindAllAsync()
    {
        return await context.Ataques.ToListAsync();
    }

    public async Task<List<Ataque>> FindByTipoAsync(Tipo tipo)
    {
        return await context.Ataques
            .Where(p => p.Tipo == tipo)
            .ToListAsync();
    }
    public void Update(Ataque ataque)
    {
        context.Ataques.Update(ataque);
    }
    public async Task<Ataque> UpdateAndCommitAsync(Ataque updatedAtaque)
    {
        context.Ataques.Update(updatedAtaque);
        await SaveChangesAsync();
        return updatedAtaque;
    }

    public void Remove(Ataque ataque)
    {
        context.Ataques.Remove(ataque);
    }
    public async Task RemoveAndCommitAsync(Ataque ataque)
    {
        context.Ataques.Remove(ataque);
        await SaveChangesAsync(); 
    }
}