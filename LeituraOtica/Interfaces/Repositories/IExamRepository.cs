using LeituraOtica.Dtos;

namespace LeituraOtica.Interfaces.Repositories;

public interface IExamRepository
{ 
    ExamDto? GetById(int id);
    List<ExamDto>? GetAll();
    bool DeleteById(int id);
    ExamDto Add(ExamDto exam);
    bool Exists(int id);
}