using BatalhaDePokemons.Application.Dtos;
using BatalhaDePokemons.Application.Dtos.Ataque;
using BatalhaDePokemons.Application.Dtos.Pokemon;
using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Application.Interfaces;
public interface IPokemonService
{
    Task<PokemonResponseDto> CriarAsync(PokemonCreationDto pokemon);
    Task<PokemonResponseDto?> ObterPorIdAsync(Guid id);
    Task<ICollection<PokemonResponseDto>> ObterTodosAsync();
    Task<PokemonResponseDto?> AtualizarAsync (Guid pokemonId, PokemonCreationDto pokemon);
    Task RemoverAsync(Guid id);
    Task VincularAtaqueAsync(Guid pokemonId, Guid ataqueId);
    Task<ICollection<AtaqueResponseDto>> ObterAtaquesPorPokemonIdAsync(Guid pokemonId);
}