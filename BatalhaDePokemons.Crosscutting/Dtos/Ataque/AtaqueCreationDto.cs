using BatalhaDePokemons.Crosscutting.Enums;

namespace BatalhaDePokemons.Crosscutting.Dtos.Ataque;

public class AtaqueCreationDto
{
    public string Nome { get; init; }
    public string Tipo { get; set; }
    public int Poder { get; set; }
    public int Precisao { get; set; }
    public int QuantUsos { get; set; }
    
}