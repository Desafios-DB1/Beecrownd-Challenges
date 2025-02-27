namespace IdentificandoCha.DTOs;

public class ContestantData
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public int Points { get; set; }
}