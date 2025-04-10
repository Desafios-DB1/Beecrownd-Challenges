using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;

namespace BatalhaDePokemons.Crosscutting.Interfaces;
public interface IPokemonService
{
    Task<Guid> CriarAsync(PokemonCreationDto pokemon);
    Task<PokemonResponseDto> ObterPorIdAsync(Guid id);
    Task<List<PokemonResponseDto>> ObterTodosComAtaquesAsync();
    Task<PokemonResponseDto> AtualizarAsync (Guid pokemonId, PokemonCreationDto pokemon);
    Task RemoverAsync(Guid id);
    Task VincularAtaqueAsync(Guid pokemonId, Guid ataqueId);
    Task<List<AtaqueResponseDto>> ObterAtaquesPorPokemonIdAsync(Guid pokemonId);
    Task<PokemonResponseDto> CurarPokemonAsync(Guid pokemonId, int newHp);
}