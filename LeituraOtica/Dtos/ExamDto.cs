namespace LeituraOtica.Dtos;

public class ExamDto
{
    public int Id { get; set; }
    public string? SubjectName { get; set; }
    public string? ResponsibleName { get; set; }
    public string? ClassName { get; set; }
    public double Value { get; set; }
}