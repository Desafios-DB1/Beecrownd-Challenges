namespace BatalhaDePokemons.Crosscutting.Dtos.Turno;

public class TurnoResponseDto
{
    public int NumeroTurno { get; set; }
    public Guid AtacanteId { get; set; }
    public Guid AlvoId { get; set; }
    public Guid AtaqueUtilizadoId { get; set; }
    public int DanoCausado  { get; set; }
}