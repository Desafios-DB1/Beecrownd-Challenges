using BatalhaDePokemons.Crosscutting.Dtos.Ataque;

namespace BatalhaDePokemons.Crosscutting.Dtos.Pokemon;

public class PokemonResponseDto
{
    public Guid PokemonId { get; init; }
    public string Nome { get; set; }
    public  int Level { get; set; }
    public  int Hp { get; set; }
    public List<AtaqueResponseDto> Ataques { get; set; }
}