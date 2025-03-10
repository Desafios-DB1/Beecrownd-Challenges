using LeituraOtica.Dtos;

namespace LeituraOtica.Interfaces.Repositories;

public interface IExamRepository
{ 
    ExamDto? GetById(Guid id);
    List<ExamDto>? GetAll();
    bool DeleteById(Guid id);
    ExamDto Add(ExamDto exam);
    bool Exists(Guid id);
}