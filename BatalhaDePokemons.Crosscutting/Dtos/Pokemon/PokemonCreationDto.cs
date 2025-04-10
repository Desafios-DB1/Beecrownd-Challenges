namespace BatalhaDePokemons.Crosscutting.Dtos.Pokemon;

public class PokemonCreationDto
{
    public string Nome { get; set; }
    public int Level { get; set; }
    public string Tipo { get; set; }
    public bool IsDesmaiado { get; set; }
    public int PontosDeVida { get; set; }
    public int Velocidade { get; set; }
    public int Ataque { get; set; }
    public int Defesa { get; set; }
}