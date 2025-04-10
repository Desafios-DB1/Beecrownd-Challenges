using BatalhaDePokemons.Crosscutting.Enums;

namespace BatalhaDePokemons.Crosscutting.Dtos.Ataque;

public class AtaqueCreationDto
{
    public required string Nome { get; init; }
    public required string Tipo { get; init; }
    public int Poder { get; init; }
    public int Precisao { get; init; }
    public int QuantUsos { get; init; }
    
}