namespace LeituraOtica.Dtos;

public class ExamDto
{
    public int Id { get; set; }
    public required string SubjectName { get; set; }
    public string? ResponsibleName { get; set; }
    public string? ClassName { get; set; }
    public required double Value { get; set; }
}