using BatalhaDePokemons.Crosscutting.Dtos.Batalha;
using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Mappers;

public static class BatalhaMapper
{
    private static BatalhaResponseDto MapToResponseDto(Batalha batalha)
    {
        return new BatalhaResponseDto
        {
            BatalhaId = batalha.BatalhaId,
            Pokemon1Id = batalha.Pokemon1Id,
            Pokemon2Id = batalha.Pokemon2Id,
            ProximoTurnoDoPokemonId = batalha.ProximoTurnoDoPokemonId,
            IsFinalizada = batalha.IsFinalizada,
            VencedorId = batalha.VencedorId,
            Turnos = TurnoMapper.MapToResponseDtos(batalha.Turnos)
        };
    }
    
    public static BatalhaDetalhadaDto MapToDetalhadaDto(Batalha batalha, Pokemon pokemon1, Pokemon pokemon2)
    {
        return new BatalhaDetalhadaDto
        {
            BatalhaId = batalha.BatalhaId,
            
            Pokemon1Id = batalha.Pokemon1Id,
            Pokemon1Nome = pokemon1.Nome,
            Pokemon1Hp = pokemon1.Status.PontosDeVida,
            
            Pokemon2Id = batalha.Pokemon2Id,
            Pokemon2Nome = pokemon2.Nome,
            Pokemon2Hp = pokemon2.Status.PontosDeVida,
            
            ProximoTurnoDoPokemonId = batalha.ProximoTurnoDoPokemonId,
            IsFinalizada = batalha.IsFinalizada,
            VencedorId = batalha.VencedorId,
            Turnos = TurnoMapper.MapToResponseDtos(batalha.Turnos)
        };
    }

    public static List<BatalhaResponseDto> MapToResponseDtos(IEnumerable<Batalha> batalhas)
    {
        return batalhas.Select(MapToResponseDto).ToList();
    }
}