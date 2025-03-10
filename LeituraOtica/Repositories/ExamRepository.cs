using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;

namespace LeituraOtica.Repositories;

public class ExamRepository : IExamRepository
{
    private static readonly List<ExamDto> Exams = [];

    public ExamDto Add(ExamDto exam)
    {
        exam.Id = Guid.NewGuid();
        Exams.Add(exam);
        return exam;
    }

    public bool Exists(Guid id)
    {
        return Exams.Any(exam => exam.Id == id);
    }

    public ExamDto? GetById(Guid id)
    {
        return Exams.FirstOrDefault(exam => exam.Id == id);
    }

    public List<ExamDto> GetAll()
    {
        return Exams;
    }
    
    public bool DeleteById(Guid id)
    {
        var exam = GetById(id);
        
        if (exam == null)
        {
            return false;
        }
        
        Exams.Remove(exam);
        return true;
    }
}