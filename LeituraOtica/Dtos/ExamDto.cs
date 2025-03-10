namespace LeituraOtica.Dtos;

public class ExamDto(double value, string? responsibleName, string? subjectName)
{
    public Guid Id { get; set; }
    public string? SubjectName { get; } = subjectName;
    public string? ResponsibleName { get; } = responsibleName;
    public double Value { get; } = value;
}