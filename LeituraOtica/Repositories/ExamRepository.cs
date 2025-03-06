using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;

namespace LeituraOtica.Repositories;

public class ExamRepository : IExamRepository
{
    private static readonly List<ExamDto> Exams = [];
    private static int _currentId;

    public ExamDto Add(ExamDto exam)
    {
        exam.Id = ++_currentId;
        Exams.Add(exam);
        return exam;
    }

    public bool Exists(int id)
    {
        return Exams.Any(exam => exam.Id == id);
    }

    public ExamDto? GetById(int id)
    {
        return Exams.FirstOrDefault(exam => exam.Id == id);
    }

    public List<ExamDto> GetAll()
    {
        return Exams;
    }
    
    public bool DeleteById(int id)
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