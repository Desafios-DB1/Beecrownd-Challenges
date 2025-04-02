using BatalhaDePokemons.Domain.Enums;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;

public class AtaqueRepository(PokemonsDbContext _context) : IAtaqueRepository
{
    public async Task AddAsync(Ataque ataque)
    {
        await _context.Ataques.AddAsync(ataque);
        await _context.SaveChangesAsync();
    }
    public async Task<Ataque?> FindByIdAsync(Guid id)
    {
        return await _context.Ataques
            .FirstOrDefaultAsync(p => p.AtaqueId == id);
    }
    
    public async Task<ICollection<Ataque>> FindAllAsync()
    {
        return await _context.Ataques.ToListAsync();
    }

    public async Task<ICollection<Ataque>> FindByTipoAsync(Tipo tipo)
    {
        return await _context.Ataques
            .Where(p => p.Tipo == tipo)
            .ToListAsync();
    }
    
    public async Task UpdateAsync(Ataque updatedAtaque)
    {
        _context.Ataques.Update(updatedAtaque);
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveAsync(Ataque ataque)
    {
        _context.Ataques.Remove(ataque);
        await _context.SaveChangesAsync();
    }
}