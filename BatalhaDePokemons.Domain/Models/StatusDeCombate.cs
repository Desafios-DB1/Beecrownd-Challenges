namespace BatalhaDePokemons.Domain.Models;
public class StatusDeCombate
{
    public StatusDeCombate() { }
    public StatusDeCombate(int hp, int spd, int def, int atk)
    {
        Hp = hp;
        Spd = spd;
        Def = def;
        Atk = atk;
    }
    
    public int Hp { get; set; }
    public int Spd { get; set; }
    public int Def { get; set; }
    public int Atk { get; set; }
}