using LeituraOtica.Controllers;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LeituraOpticaTest.Controllers;

public class ExamControllerTest
{
    private readonly Mock<IExamService> _examService;
    private readonly ExamController _examController;

    public ExamControllerTest()
    {
        _examService = new Mock<IExamService>();
        _examController = new ExamController(_examService.Object);
    }

    [Fact]
    public void AddExam_ReturnsOk_WhenSuccess()
    {
        var exam = new ExamDto(10, "Teacher", "Science");
        var successResult = OperationResult.Success(exam);
        _examService.Setup(x => x.AddExam(exam)).Returns(successResult);
        
        var result = _examController.AddExam(exam);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(exam, okResult.Value);
    }

    [Fact]
    public void AddExam_ReturnsBadRequest_WhenFail()
    {
        var exam = new ExamDto(10, "Teacher", "Science");
        var failResult = OperationResult.Failure("Ocorreu um erro ao salvar a prova!");
        _examService.Setup(x => x.AddExam(exam)).Returns(failResult);
        
        var result = _examController.AddExam(exam);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Ocorreu um erro ao salvar a prova!", badRequestResult.Value);
    }

    [Fact]
    public void GetExams_ReturnsOk_WhenSuccess()
    {
        var exams = new List<ExamDto>
        {
            new(10, "Teacher", "Science")
        };
        _examService.Setup(x=>x.GetAllExams()).Returns(exams);
        
        var result = _examController.GetExams();
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(exams, okResult.Value);
    }

    [Fact]
    public void GetExams_ReturnsNoContent_WhenFail()
    {
        _examService.Setup(x => x.GetAllExams()).Returns([]);
        
        var result = _examController.GetExams();
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void GetExam_ReturnsOk_WhenSuccess()
    {
        var exam = new ExamDto(10, "Teacher", "Science");
        _examService.Setup(x => x.GetExam(exam.Id)).Returns(exam);
        
        var result = _examController.GetExam(exam.Id);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(exam, okResult.Value);
    }

    [Fact]
    public void GetExam_ReturnNotFound_WhenFail()
    {
        var examId = Guid.NewGuid();
        _examService.Setup(x=>x.GetExam(examId)).Returns((ExamDto?)null);
        
        var result = _examController.GetExam(examId);
        
        Assert.IsType<NotFoundObjectResult>(result);
    }
}