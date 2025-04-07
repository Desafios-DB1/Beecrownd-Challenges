using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BatalhaDePokemons.Crosscutting.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EstadoAtual
{
    [EnumMember(Value = "Ativo")]
    Ativo,
    [EnumMember(Value = "Desmaiado")]
    Desmaiado,
    [EnumMember(Value = "Queimado")]
    Queimado
}