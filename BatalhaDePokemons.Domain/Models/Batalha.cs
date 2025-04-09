namespace BatalhaDePokemons.Domain.Models;

public class Batalha
{
    public Guid BatalhaId { get; init; } = Guid.NewGuid();
    public Guid Pokemon1Id { get; init; }
    public Guid Pokemon2Id { get; init; }
    public Guid? VencedorId { get; set; }
    public bool IsFinalizada { get; set; }
    public Guid? ProximoTurnoDoPokemonId { get; set; }
    public ICollection<Turno> Turnos { get; init; } = [];
}