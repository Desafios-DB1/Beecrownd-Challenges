using BatalhaDePokemons.Crosscutting.Dtos.Batalha;

namespace BatalhaDePokemons.Crosscutting.Interfaces;

public interface IBatalhaService
{
    Task<Guid> IniciarBatalha(Guid atacanteId, Guid defensorId);
    Task ExecutarTurno(Guid batalhaId, Guid atacanteId, Guid ataqueId);
    Task EncerrarBatalhaAsync(Guid batalhaId, Guid desistenteId);
    Task<List<BatalhaResponseDto>> ObterTodasBatalhas();
    Task<BatalhaDetalhadaDto> ObterBatalhaDetalhadaAsync(Guid batalhaId);
}