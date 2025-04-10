using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra.Repositories;

public class TurnoRepository(PokemonsDbContext context) : ITurnoRepository
{
    public async Task SalvarAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<Guid> AdicionarAsync(Turno turno)
    {
        await context.Turnos.AddAsync(turno);
        return turno.TurnoId;
    }

    public async Task<Guid> AdicionarESalvarAsync(Turno turno)
    {
        await context.Turnos.AddAsync(turno);
        await SalvarAsync();
        return turno.TurnoId;
    }

    public async Task<IEnumerable<Turno>> ObterPorIdDaBatalhaAsync(Guid batalhaId)
    {
        return await context.Turnos
            .Where(turno => turno.BatalhaId == batalhaId)
            .OrderBy(t => t.NumeroTurno)
            .ToListAsync();
    }
}