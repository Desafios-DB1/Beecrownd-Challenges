using BatalhaDePokemons.Crosscutting.Constantes;
using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Crosscutting.Exceptions;
using BatalhaDePokemons.Crosscutting.Exceptions.Pokemon;

namespace BatalhaDePokemons.Domain.Models;

public class Pokemon
{
    public Pokemon()
    {
    }

    public Pokemon(string nome, int level, Tipo tipo, int hp, int spd, int def, int atk)
    {
        Nome = nome;
        Level = level;
        Tipo = tipo;
        Status = new StatusDeCombate(hp, spd, def, atk);
    }

    public Guid PokemonId { get; init; } = Guid.NewGuid();
    public string Nome { get; set; }
    public int Level { get; set; }
    public bool IsDesmaiado { get; set; }
    public Tipo Tipo { get; set; }
    public StatusDeCombate Status { get; init; }
    public ICollection<Ataque> Ataques { get; init; }
    public ICollection<PokemonAtaque> PokemonAtaques { get; init; } = [];
    public DateTime DataHoraCriacao { get; init; } = DateTime.Now;

    public void VerificarSePodeAprenderAtaque(Ataque ataque)
    {
        if (ataque.Tipo != Tipo)
            throw new TiposDiferentesException(ExceptionMessages.TiposDiferentes);
        if (PokemonAtaques.Count >= 4)
            throw new MaxAtaquesException(ExceptionMessages.MaxAtaquesPossivel);
        if (JaAprendeuAtaque(ataque.AtaqueId))
            throw new JaAprendeuAtaqueException(ExceptionMessages.JaAprendeuAtaque(ataque.Nome));
    }

    private bool JaAprendeuAtaque(Guid ataqueId)
    {
        return PokemonAtaques.Any(pa => pa.AtaqueId == ataqueId);
    }

    public void RecebeDano(int poder)
    {
        Status.Hp -= poder;
        if (Status.Hp <= 0)
            IsDesmaiado = true;
    }

    public void Curar(int newHp)
    {
        Status.Hp = newHp;
    }
}

