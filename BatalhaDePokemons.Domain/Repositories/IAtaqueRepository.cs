using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface IAtaqueRepository : IRepository<Ataque>
{
    Task<List<Ataque>> ObterPorTipoAsync(Tipo tipo);
}