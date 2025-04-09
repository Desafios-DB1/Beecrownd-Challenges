namespace BatalhaDePokemons.Crosscutting.Constantes;

public static class ExceptionMessages
{
    public static string PokemonNaoEncontrado(Guid id) =>
        $"Pokemon com o id {id} não foi encontrado";
    
    public static string AtaqueNaoEncontrado(Guid id) =>
        $"Ataque com o id {id} não foi encontrado";
    public static string BatalhaNaoEncontrada(Guid id) =>
        $"Batalha com o id {id} não foi encontrada";
    
    public static string TipoInvalido(string tipo) =>
        $"Tipo {tipo} é inválido";
    
    public static string MaxAtaquesPossivel =>
        "O pokemon já possui o máximo de ataques possível";

    public static string TiposDiferentes =>
        "O ataque deve ser do mesmo tipo que o pokemon!";

    public static string JaAprendeuAtaque(string ataque) =>
        $"O pokemon já aprendeu o ataque {ataque}!";

    public static string PokemonsDevemEstarSaudaveis =>
        "Ambos pokemons devem estar saudáveis para batalhar!";

    public static string PokemonsJaBatalhando =>
        "Os pokemons já estão em batalha!";

    public static string BatalhaJaFinalizada =>
        "Essa batalha já acabou.";

    public static string PokemonAtacandoForaDoTurno =>
        "Não é a vez deste pokemon atacar";

    public static string PokemonNaoConheceAtaque =>
        "O pokemon não conhece esse ataque!";

    public static string PokemonNaoParticipaDaBatalha =>
        "O pokemon não está participando da batalha";
}