namespace BatalhaDePokemons.Domain.Models;

public class Turno
{
    public Guid TurnoId { get; init; } = Guid.NewGuid();
    public Guid BatalhaId { get; init; }
    public Batalha Batalha { get; init; }
    
    public int NumeroTurno { get; init; }
    
    public Guid AtacanteId { get; init; }
    public Guid AlvoId { get; init; }
    public Guid AtaqueUtilizadoId { get; init; }
    
    public int DanoCausado { get; init; }
    public DateTime DataHoraCriacao { get; init; } = DateTime.Now;
}