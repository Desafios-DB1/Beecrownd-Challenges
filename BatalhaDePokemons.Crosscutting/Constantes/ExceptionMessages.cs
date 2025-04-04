namespace BatalhaDePokemons.Crosscutting.Constantes;

public static class ExceptionMessages
{
    public static string PokemonNaoEncontrado(Guid id) =>
        $"Pokemon com o id {id} não foi encontrado";
    
    public static string AtaqueNaoEncontrado(Guid id) =>
        $"Ataque com o id {id} não foi encontrado";
    
    public static string TipoInvalido(string tipo) =>
        $"Tipo {tipo} é inválido";
    
    public static string MaxAtaquesPossivel =>
        "O pokemon já possui o máximo de ataques possível";
}