using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;

namespace LeituraOtica.Services;

public class StudentAnswerService (IStudentAnswerRepository studentAnswerRepository, 
    IValidationService validationService,
    IExamCorrectionService examCorrectionService,
    IOpticalConversionService opticalConversionService) : IStudentAnswerService
{
    public OperationResult AddStudentAnswer(StudentAnswerDto studentAnswer)
    {
        var studentAnswerValidation = validationService.Validate(studentAnswer);
        if (!studentAnswerValidation.IsSuccess)
            return studentAnswerValidation;

        var studentAnswerWithGrade = new StudentAnswerWithGradeDto(
            studentAnswer.ExamId,
            studentAnswer.AnswerKeyId,
            GetFormattedAnswers(studentAnswer.Answers));
        
        studentAnswerWithGrade.Grade = GetGrade(studentAnswerWithGrade);
        
        var newStudentAnswer = studentAnswerRepository.Add(studentAnswerWithGrade);
        return OperationResult.Success(newStudentAnswer);
    }

    public StudentAnswerWithGradeDto? GetStudentAnswerById(Guid id)
    {
        return studentAnswerRepository.GetById(id);
    }

    public List<StudentAnswerWithGradeDto> GetAllStudentsAnswers()
    {
        var studentsAnswers = studentAnswerRepository.GetAll();
        return studentsAnswers ?? [];
    }

    private Dictionary<int, char> GetFormattedAnswers(List<int[]> answers)
    {
        var formattedAnswers = opticalConversionService.ConvertNumbersToLetters(answers);
        return formattedAnswers;
    }

    private double GetGrade(StudentAnswerWithGradeDto studentAnswer)
    {
        var grade = examCorrectionService.Correction(studentAnswer);
        return grade;
    }
}