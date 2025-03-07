using FluentValidation;
using FluentValidation.Results;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Services;
using Moq;

namespace LeituraOpticaTest.Services;

public class AnswerKeyServiceTest
{
    private readonly Mock<IAnswerKeyRepository> _mockRepository = new();
    private readonly Mock<IValidator<AnswerKeyDto>> _mockValidator = new();
    private readonly Mock<IExamService> _mockExamService = new();
    private readonly IAnswerKeyService _service;

    public AnswerKeyServiceTest()
    {
        _service = new AnswerKeyService(
            _mockRepository.Object,
            _mockValidator.Object,
            _mockExamService.Object);
    }

    public AnswerKeyServiceTest(IAnswerKeyService service)
    {
        _service = service;
    }

    [Fact]
    public void SaveAnswerKey_ReturnFailure_WhenValidationFails()
    {
        var validationFailures = new List<ValidationFailure>
        {
            new ("Answers", "Respostas inválidas")
        };
        _mockValidator.Setup(v => v.Validate(It.IsAny<AnswerKeyDto>()))
            .Returns(new ValidationResult(validationFailures));
        var answerKey = new AnswerKeyDto(1, new Dictionary<int, char>());

        var result = _service.SaveAnswerKey(answerKey);
        
        Assert.False(result.IsSuccess);
        Assert.Equal("Respostas inválidas", result.ErrorMessage);
        _mockRepository.Verify(r => r.Save(It.IsAny<AnswerKeyDto>()), Times.Never());
    }

    [Fact]
    public void SaveAnswerKey_ReturnFailure_WhenExamDoesNotExist()
    {
        _mockValidator.Setup(v => v.Validate(It.IsAny<AnswerKeyDto>()))
            .Returns(new ValidationResult());
        
        _mockExamService.Setup(e => e.GetExam(It.IsAny<int>()))
            .Returns(null as ExamDto);
        
        var answerKey = new AnswerKeyDto(1, new Dictionary<int, char>());
        
        var result = _service.SaveAnswerKey(answerKey);
        
        Assert.False(result.IsSuccess);
        Assert.Equal("A prova não existe!", result.ErrorMessage);
        _mockRepository.Verify(r => r.Save(It.IsAny<AnswerKeyDto>()), Times.Never());
    }

    [Fact]
    public void SaveAnswerKey_ReturnSuccess_WhenValidationPasses()
    {
        _mockValidator.Setup(v=>v.Validate(It.IsAny<AnswerKeyDto>()))
            .Returns(new ValidationResult());
        
        _mockExamService.Setup(e => e.GetExam(It.IsAny<int>()))
            .Returns(new ExamDto(10, "João", "Science"));
        
        var answerKey = new AnswerKeyDto(1, new Dictionary<int, char>());
        
        var result = _service.SaveAnswerKey(answerKey);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(answerKey, result.Data);
        _mockRepository.Verify(r => r.Save(It.IsAny<AnswerKeyDto>()), Times.Once());
    }
}