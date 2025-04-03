using BatalhaDePokemons.Domain.Enums;
using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface IAtaqueRepository : IRepository<Ataque>
{
    Task<ICollection<Ataque>> FindByTipoAsync(Tipo tipo);
}