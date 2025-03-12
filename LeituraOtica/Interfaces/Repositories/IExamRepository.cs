using LeituraOtica.Dtos;

namespace LeituraOtica.Interfaces.Repositories;

public interface IExamRepository
{ 
    ExamDto? GetById(Guid id);
    List<ExamDto>? GetAll();
    ExamDto Add(ExamDto exam);
    bool Exists(Guid id);
}