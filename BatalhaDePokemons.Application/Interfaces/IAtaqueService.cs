using BatalhaDePokemons.Application.Dtos;
using BatalhaDePokemons.Application.Dtos.Ataque;
using BatalhaDePokemons.Domain.Enums;
using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Application.Interfaces;

public interface IAtaqueService
{
    Task<AtaqueResponseDto> CriarAsync(AtaqueCreationDto ataque);
    Task<AtaqueResponseDto?> ObterPorIdAsync(Guid id);
    Task<ICollection<AtaqueResponseDto>> ObterTodosAsync();
    Task<ICollection<AtaqueResponseDto>> ObterPorTipoAsync(Tipo tipo);
    Task<AtaqueResponseDto?> AtualizarAsync(Guid ataqueId, AtaqueCreationDto ataque);
    Task RemoverAsync(Guid id);
}