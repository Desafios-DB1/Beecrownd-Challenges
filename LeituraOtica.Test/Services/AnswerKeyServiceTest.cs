using FluentValidation;
using FluentValidation.Results;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;
using LeituraOtica.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LeituraOpticaTest.Services;

public class AnswerKeyServiceTest
{
    private readonly Mock<IAnswerKeyRepository> _mockRepository;
    private readonly Mock<IValidationService> _validationService;
    private readonly AnswerKeyService _service;

    public AnswerKeyServiceTest()
    {
        _mockRepository = new Mock<IAnswerKeyRepository>();
        _validationService = new Mock<IValidationService>();
        _service = new AnswerKeyService(_mockRepository.Object, _validationService.Object);
    }

    [Fact]
    public void SaveAnswerKey_ReturnsSuccess_WhenValid()
    {
        var answerKey = new AnswerKeyDto(Guid.NewGuid(), new Dictionary<int, char>
        {
            {1, 'A'}
        });
        var successResult = OperationResult.Success(answerKey);
        _validationService.Setup(x=>x.Validate(answerKey)).Returns(successResult);
        _mockRepository.Setup(x=>x.Save(answerKey)).Returns(answerKey);
        
        var result = _service.SaveAnswerKey(answerKey);
        
        var okResult = Assert.IsType<OperationResult>(result);
        Assert.Equal(answerKey, okResult.Data);
    }
    
    [Fact]
    public void SaveAnswerKey_ReturnFailure_WhenValidationFails()
    {
        var answerKey = new AnswerKeyDto(Guid.NewGuid(), new Dictionary<int, char>());
        _validationService.Setup(v => v.Validate(It.IsAny<AnswerKeyDto>()))
            .Returns(OperationResult.Failure("Respostas inválidas!"));
        
        var result = _service.SaveAnswerKey(answerKey);
        
        Assert.False(result.IsSuccess);
        Assert.Equal("Respostas inválidas!", result.ErrorMessage);
        _mockRepository.Verify(r => r.Save(It.IsAny<AnswerKeyDto>()), Times.Never());
    }

    [Fact]
    public void SaveAnswerKey_ReturnFailure_WhenExamDoesNotExist()
    {
        _validationService.Setup(v => v.Validate(It.IsAny<AnswerKeyDto>()))
            .Returns(OperationResult.Failure("Exam does not exist!"));
        var answerKey = new AnswerKeyDto(Guid.NewGuid(), new Dictionary<int, char>());
        
        var result = _service.SaveAnswerKey(answerKey);
        
        Assert.False(result.IsSuccess);
        Assert.Equal("Exam does not exist!", result.ErrorMessage);
        _mockRepository.Verify(r => r.Save(It.IsAny<AnswerKeyDto>()), Times.Never());
    }

    [Fact]
    public void GetAnswerKey_ReturnsAnswerKey_WhenValid()
    {
        var answerKey = new AnswerKeyDto(Guid.NewGuid(), new Dictionary<int, char>());
        _mockRepository.Setup(x=>x.GetById(answerKey.Id)).Returns(answerKey);
        
        var result = _service.GetAnswerKey(answerKey.Id);
        
        Assert.Equal(answerKey, result);
    }

    [Fact]
    public void GetAnswerKey_ReturnNull_WhenInvalid()
    {
        var answerKeyId = Guid.NewGuid();
        _mockRepository.Setup(x=>x.GetById(answerKeyId)).Returns((AnswerKeyDto?)null);
        
        var result = _service.GetAnswerKey(answerKeyId);
        
        Assert.Null(result);
    }

    [Fact]
    public void GetAllAnswerKeys_ReturnsAllAnswerKeys_WhenValid()
    {
        var answersKeys = new List<AnswerKeyDto>
        {
            new(Guid.NewGuid(), new Dictionary<int, char>())
        };
        _mockRepository.Setup(x=>x.GetAll()).Returns(answersKeys);
        
        var result = _service.GetAllAnswerKeys();
        
        Assert.Equal(answersKeys, result);
    }

    [Fact]
    public void GetAllAnswerKeys_ReturnsEmpty_WhenInvalid()
    {
        _mockRepository.Setup(x=>x.GetAll()).Returns([]);
        
        var result = _service.GetAllAnswerKeys();
        
        Assert.Empty(result);
    }

    [Fact]
    public void GetAnswerKeyAnswers_ReturnsDictionaryOfAnswerKeys_WhenValid()
    {
        var answerKey = new AnswerKeyDto(Guid.NewGuid(), new Dictionary<int, char>
        {
            {1, 'A'}
        });
        _mockRepository.Setup(x=>x.GetById(answerKey.Id)).Returns(answerKey);
        
        var result = _service.GetAnswerKeyAnswers(answerKey.Id);
        
        Assert.Equal(answerKey.Answers, result);
    }

    [Fact]
    public void GetAnswerKeyAnswers_ReturnNull_WhenInvalid()
    {
        var answerKeyId = Guid.NewGuid();
        _mockRepository.Setup(x=>x.GetById(answerKeyId)).Returns((AnswerKeyDto?)null);
        
        var result = _service.GetAnswerKeyAnswers(answerKeyId);
        
        Assert.Null(result);
    }

    [Fact]
    public void GetTotalQuestions_ReturnsTotalQuestions_WhenValid()
    {
        var answerKey = new AnswerKeyDto(Guid.NewGuid(), new Dictionary<int, char>
        {
            {1, 'A'}
        });
        _mockRepository.Setup(x=>x.GetById(answerKey.Id)).Returns(answerKey);
        
        var result = _service.GetTotalQuestions(answerKey.Id);
        
        Assert.Equal(answerKey.Answers!.Count, result);
    }

    [Fact]
    public void GetTotalQuestions_Returns0_WhenInvalid()
    {
        var answerKeyId = Guid.NewGuid();
        _mockRepository.Setup(x=>x.GetById(answerKeyId)).Returns((AnswerKeyDto?)null);
        
        var result = _service.GetTotalQuestions(answerKeyId);
        
        Assert.Equal(0, result);
    }
}