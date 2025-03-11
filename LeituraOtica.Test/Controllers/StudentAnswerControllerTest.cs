using LeituraOtica.Controllers;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LeituraOpticaTest.Controllers;

public class StudentAnswerControllerTest
{
    private readonly Mock<IStudentAnswerService> _serviceMock;
    private readonly StudentAnswerController _controller;

    public StudentAnswerControllerTest()
    {
        _serviceMock = new Mock<IStudentAnswerService>();
        _controller = new StudentAnswerController(_serviceMock.Object);
    }

    [Fact]
    public void AddStudentAnswer_ReturnsOkResult_WhenSuccess()
    {
        var examId = Guid.NewGuid();
        var answerKey = Guid.NewGuid();
        var answers = new List<int[]>{
            ([130, 20, 30, 40, 50]),
            ([130, 150, 40, 255, 140]),
            ([150, 170, 0, 200, 255]),
            ([0, 255, 255, 230, 128])
        };
        var studentAnswer = new StudentAnswerDto(examId, answerKey, answers);
        var successResult = OperationResult.Success(studentAnswer);
        _serviceMock.Setup(x => x.AddStudentAnswer(studentAnswer)).Returns(successResult);
        
        var result = _controller.AddStudentAnswer(studentAnswer);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<OkObjectResult>(okResult);
        Assert.Equal(studentAnswer, okResult.Value);
    }

    [Fact]
    public void AddStudentAnswer_ReturnsBadRequest_WhenFail()
    {
        var studentAnswer = new StudentAnswerDto(Guid.NewGuid(), Guid.NewGuid(), []);
        var failResult = OperationResult.Failure("Ocorreu um erro ao salvar as respostas!");
        _serviceMock.Setup(x => x.AddStudentAnswer(studentAnswer)).Returns(failResult);
        
        var result = _controller.AddStudentAnswer(studentAnswer);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<BadRequestObjectResult>(badRequestResult);
    }

    [Fact]
    public void GetAllStudentsAnswers_ReturnsOkResult_WhenSuccess()
    {
        var studentsAnswers = new List<StudentAnswerWithGradeDto>
        {
            new (Guid.NewGuid(), Guid.NewGuid(), new Dictionary<int, char>())
        };
        _serviceMock.Setup(x => x.GetAllStudentsAnswers()).Returns(studentsAnswers);
        
        var result = _controller.GetAllStudentsAnswers();
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<OkObjectResult>(okResult);
        Assert.Equal(studentsAnswers, okResult.Value);
    }

    [Fact]
    public void GetAllStudentsAnswers_ReturnsNoContent_WhenFail()
    {
        _serviceMock.Setup(x => x.GetAllStudentsAnswers()).Returns([]);
        
        var result = _controller.GetAllStudentsAnswers();
        
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.IsType<NoContentResult>(noContentResult);
    }

    [Fact]
    public void GetStudentAnswerById_ReturnsOkResult_WhenSuccess()
    {
        var studentAnswer = new StudentAnswerWithGradeDto(Guid.NewGuid(), Guid.NewGuid(), new Dictionary<int, char>());
        _serviceMock.Setup(x => x.GetStudentAnswerById(studentAnswer.Id)).Returns(studentAnswer);
        
        var result = _controller.GetStudentAnswerById(studentAnswer.Id);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<OkObjectResult>(okResult);
    }

    [Fact]
    public void GetStudentAnswerById_ReturnsNotFound_WhenFail()
    {
        var studentAnswerId = Guid.NewGuid();
        _serviceMock.Setup(x => x.GetStudentAnswerById(studentAnswerId)).Returns((StudentAnswerWithGradeDto?)null);
        
        var result = _controller.GetStudentAnswerById(studentAnswerId);
        
        Assert.IsType<NotFoundObjectResult>(result);
    }
}