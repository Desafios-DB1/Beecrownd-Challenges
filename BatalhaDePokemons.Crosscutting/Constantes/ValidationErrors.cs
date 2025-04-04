using Bogus;

namespace BatalhaDePokemons.Crosscutting.Constantes;

public static class ValidationErrors
{
    public static string CampoObrigatorio => "{PropertyName} é obrigatório e deve ser informado!";
    public static string TamanhoMaximo(int max) => "{PropertyName} deve ter no máximo " + max + " caracteres!";
    public static string EnumInvalido=> "{PropertyName} não é válido!"; 
    public static string ValorMinimo(int min) => "{PropertyName} deve ser maior que " + min + "!";
    public static string ValorMaximo(int max) => "{PropertyName} deve ser menor que " + max + "!";
    
}