using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Mappers;

public static class PokemonMapper
{
    public static PokemonResponseDto MapToResponseDto(Pokemon pokemon)
    {
        return new PokemonResponseDto
        {
            PokemonId = pokemon.PokemonId,
            Nome = pokemon.Nome,
            Level = pokemon.Nivel,
            Hp = pokemon.Status.PontosDeVida,
            Ataques = pokemon.Ataques?.Select(pa => new AtaqueResponseDto
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
        return new PokemonCreationDto
        {
            Nome = pokemon.Nome,
            Level = pokemon.Nivel,
            IsDesmaiado = pokemon.IsDesmaiado,
            PontosDeVida = pokemon.Status.PontosDeVida,
            Ataque = pokemon.Status.Ataque,
            Defesa = pokemon.Status.Defesa,
            Velocidade = pokemon.Status.Velocidade,
            Tipo = pokemon.Tipo.ToString()
        };
    }
}