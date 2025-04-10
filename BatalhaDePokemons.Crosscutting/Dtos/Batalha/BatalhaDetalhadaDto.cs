using BatalhaDePokemons.Crosscutting.Dtos.Turno;

namespace BatalhaDePokemons.Crosscutting.Dtos.Batalha;

public class BatalhaDetalhadaDto
{
    public Guid BatalhaId { get; set; }
    public Guid Pokemon1Id { get; set; }
    public string Pokemon1Nome { get; set; }
    public int Pokemon1Hp { get; set; }
    
    public Guid Pokemon2Id { get; set; }
    public string Pokemon2Nome { get; set; }
    public int Pokemon2Hp { get; set; }
    
    public Guid? ProximoTurnoDoPokemonId { get; set; }
    public bool IsFinalizada { get; set; }
    public Guid? VencedorId { get; set; }
    public List<TurnoResponseDto> Turnos { get; set; }
}