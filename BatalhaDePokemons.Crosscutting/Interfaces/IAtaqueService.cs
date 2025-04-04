using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Enums;

namespace BatalhaDePokemons.Crosscutting.Interfaces;

public interface IAtaqueService
{
    Task<Guid> CriarAsync(AtaqueCreationDto ataque);
    Task<AtaqueResponseDto?> ObterPorIdAsync(Guid id);
    Task<List<AtaqueResponseDto>> ObterTodosAsync();
    Task<List<AtaqueResponseDto>> ObterPorTipoAsync(Tipo tipo);
    Task<AtaqueResponseDto> AtualizarAsync(Guid ataqueId, AtaqueCreationDto ataque);
    Task RemoverAsync(Guid id);
}