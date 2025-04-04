using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BatalhaDePokemons.Crosscutting.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Tipo
{
    [EnumMember(Value = "Agua")]
    Agua,
    [EnumMember(Value = "Fogo")]
    Fogo,
    [EnumMember(Value = "Planta")]
    Planta
}