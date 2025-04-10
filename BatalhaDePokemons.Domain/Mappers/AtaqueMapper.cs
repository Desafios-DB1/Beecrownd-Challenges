using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Mappers;

public static class AtaqueMapper
{
    public static AtaqueResponseDto MapToResponseDto(Ataque ataque)
    {
        return new AtaqueResponseDto
        {
            AtaqueId = ataque.AtaqueId,
            Nome = ataque.Nome,
            Poder = ataque.Poder,
            QuantUsos = ataque.QuantUsos,
            Tipo = ataque.Tipo
        };
    }

    public static AtaqueCreationDto MapToCreationDto(Ataque ataque)
    {
        return new AtaqueCreationDto
        {
            Nome = ataque.Nome,
            Poder = ataque.Poder,
            QuantUsos = ataque.QuantUsos,
            Tipo = ataque.Tipo.ToString(),
            Precisao = ataque.Precisao
        };
    }
}