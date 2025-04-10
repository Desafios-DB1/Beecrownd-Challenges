using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;

public class AtaqueRepository(PokemonsDbContext context) : IAtaqueRepository
{
    public async Task SalvarAsync()
    { 
        await context.SaveChangesAsync();
    }

    public async Task<Guid> AdicionarAsync(Ataque ataque)
    {
        await context.Ataques.AddAsync(ataque);
        return ataque.AtaqueId;
    }
    public async Task<Guid> AdicionarESalvarAsync(Ataque ataque)
    {
        await context.Ataques.AddAsync(ataque);
        await SalvarAsync(); 
        return ataque.AtaqueId;
    }
    public async Task<Ataque?> ObterPorIdAsync(Guid id)
    {
        return await context.Ataques
            .FirstOrDefaultAsync(p => p.AtaqueId == id);
    }
    
    public async Task<List<Ataque>> ObterTodosAsync()
    {
        return await context.Ataques.ToListAsync();
    }

    public async Task<List<Ataque>> ObterPorTipoAsync(Tipo tipo)
    {
        return await context.Ataques
            .Where(p => p.Tipo == tipo)
            .ToListAsync();
    }
    public void Atualizar(Ataque ataque)
    {
        context.Ataques.Update(ataque);
    }
    public async Task<Ataque> AtualizarESalvarAsync(Ataque updatedAtaque)
    {
        context.Ataques.Update(updatedAtaque);
        await SalvarAsync();
        return updatedAtaque;
    }

    public void Remover(Ataque ataque)
    {
        context.Ataques.Remove(ataque);
    }
    public async Task RemoverESalvarAsync(Ataque ataque)
    {
        context.Ataques.Remove(ataque);
        await SalvarAsync(); 
    }
}