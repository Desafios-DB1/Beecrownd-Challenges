using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Mappers;

public static class PokemonMapper
{
    public static PokemonResponseDto MapToResponseDto(Pokemon pokemon)
    {
        return new PokemonResponseDto()
        {
            Nome = pokemon.Nome,
            Level = pokemon.Level,
            Hp = pokemon.Status.Hp,
            Ataques = pokemon.Ataques?
                .Select(pa => new AtaqueResponseDto()
                {
                    Nome = pa.Nome,
                    Poder = pa.Poder,
                    QuantUsos = pa.QuantUsos,
                    Tipo = pa.Tipo
                }).ToList() ?? []
        };
    }

    public static PokemonCreationDto MapToCreationDto(Pokemon pokemon)
    {
        return new PokemonCreationDto()
        {
            Nome = pokemon.Nome,
            Level = pokemon.Level,
            Hp = pokemon.Status.Hp,
            Atk = pokemon.Status.Atk,
            Def = pokemon.Status.Def,
            Spd = pokemon.Status.Spd,
            Tipo = pokemon.Tipo.ToString()
        };
    }
}