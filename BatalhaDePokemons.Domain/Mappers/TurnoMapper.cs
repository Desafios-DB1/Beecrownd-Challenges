using BatalhaDePokemons.Crosscutting.Dtos.Turno;
using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Mappers;

public static class TurnoMapper
{
    private static TurnoResponseDto MapToResponseDto(Turno turno)
    {
        return new TurnoResponseDto
        {
            NumeroTurno = turno.NumeroTurno,
            AtacanteId = turno.AtacanteId,
            AlvoId = turno.AlvoId,
            AtaqueUtilizadoId = turno.AtaqueUtilizadoId,
            DanoCausado = turno.DanoCausado
        };
    }

    public static List<TurnoResponseDto> MapToResponseDtos(IEnumerable<Turno> turnos)
    {
        return turnos.Select(MapToResponseDto).ToList();
    }
}