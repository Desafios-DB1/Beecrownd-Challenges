using System.ComponentModel.DataAnnotations;

namespace IdentificandoCha.DTOs;

public class ContestantAnswer
{
    [Required]
    public int ContestantId { get; init; }
    [Range(1, 5, ErrorMessage = "O valor deve estar entre 1 e 5")]
    public int Answer { get; init; }
}