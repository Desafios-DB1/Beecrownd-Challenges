using LeituraOtica.Dtos;

namespace LeituraOtica.Interfaces.Services;

public interface IExamCorrectionService
{
    double Correction(StudentAnswerDto input);
}