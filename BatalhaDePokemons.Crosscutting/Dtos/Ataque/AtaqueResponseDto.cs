using BatalhaDePokemons.Crosscutting.Enums;

namespace BatalhaDePokemons.Crosscutting.Dtos.Ataque;

public class AtaqueResponseDto
{
    public Guid AtaqueId { get; init; }
    public string Nome { get; init; }
    public Tipo Tipo { get; init; }
    public int Poder { get; init; }
    public int QuantUsos { get; init; }
}