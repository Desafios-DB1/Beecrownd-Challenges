namespace BatalhaDePokemons.Domain.Models;
public class StatusDeCombate
{
    public StatusDeCombate() { }
    public StatusDeCombate(int pontosDeVida, int velocidade, int def, int ataque)
    {
        PontosDeVida = pontosDeVida;
        Velocidade = velocidade;
        Defesa = def;
        Ataque = ataque;
    }
    
    public int PontosDeVida { get; set; }
    public int Velocidade { get; set; }
    public int Defesa { get; set; }
    public int Ataque { get; set; }
}