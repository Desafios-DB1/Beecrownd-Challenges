﻿namespace BatalhaDePokemons.Crosscutting.Dtos.Pokemon;

public class PokemonCreationDto
{
    public string Nome { get; set; }
    public int Level { get; set; }
    public string Tipo { get; set; }
    public bool IsDesmaiado { get; set; }
    public int Hp { get; set; }
    public int Spd { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
}