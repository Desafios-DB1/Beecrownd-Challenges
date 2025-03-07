using LeituraOtica.Controllers;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LeituraOpticaTest.Controllers;

public class AnswerKeyControllerTest
{
    private readonly Mock<IAnswerKeyService> _answerKeyService;
    private readonly AnswerKeyController _controller;

    public AnswerKeyControllerTest()
    {
        _answerKeyService = new Mock<IAnswerKeyService>();
        _controller = new AnswerKeyController(_answerKeyService.Object);
    }

    [Fact]
    public void SaveAnswerKey_ReturnsOk_WhenSuccess()
    {
        //Arrange
        var answerKeyDto = new AnswerKeyDto(1, new Dictionary<int, char> { { 1, 'A' } });
        var successResult = OperationResult.Success(answerKeyDto);
        _answerKeyService.Setup(x => x.SaveAnswerKey(answerKeyDto)).Returns(successResult);

        //Act
        var result = _controller.SaveAnswerKey(answerKeyDto);
        
        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(answerKeyDto, okResult.Value);
    }

    [Fact]
    public void SaveAnswerKey_ReturnsBadRequest_WhenFail()
    {
        //Arrange
        var answerKeyDto = new AnswerKeyDto(1, new Dictionary<int, char>{{1, 'A'}});
        var failResult = OperationResult.Failure("Erro ao salvar o gabarito!");
        _answerKeyService.Setup(x => x.SaveAnswerKey(answerKeyDto)).Returns(failResult);
        
        //Act
        var result = _controller.SaveAnswerKey(answerKeyDto);
        
        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.Value);
        Assert.Equal("Erro ao salvar o gabarito!", badRequestResult.Value);
    }

    [Fact]
    public void GetAnswerKeys_ReturnsOk_WhenSuccess()
    {
        //Arrange
        var answerKeys = new List<AnswerKeyDto>
        {
            new(1, new Dictionary<int, char> { { 1, 'A' } })
        };
        _answerKeyService.Setup(x => x.GetAllAnswerKeys()).Returns(answerKeys);
        
        //Act
        var result = _controller.GetAnswerKeys();
        
        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(answerKeys, okResult.Value);
    }

    [Fact]
    public void GetAnswerKeys_ReturnsNoContent_WhenFail()
    {
        //Arrange
        _answerKeyService.Setup(x => x.GetAllAnswerKeys()).Returns([]);
        
        //Act
        var result = _controller.GetAnswerKeys();
        
        //Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void GetAnswerKey_ReturnsOk_WhenSuccess()
    {
        //Arrange
        const int answerKeyId = 1;
        var answerKey = new AnswerKeyDto(answerKeyId, new Dictionary<int, char> { { 1, 'A' } });
        _answerKeyService.Setup(x => x.GetAnswerKey(answerKeyId)).Returns(answerKey);
        
        //Act
        var result = _controller.GetAnswerKey(answerKeyId);
        
        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(answerKey, okResult.Value);
    }

    [Fact]
    public void GetAnswerKey_ReturnsNotFound_WhenFail()
    {
        //Arrange
        const int answerKeyId = 1;
        _answerKeyService.Setup(x => x.GetAnswerKey(answerKeyId)).Returns((AnswerKeyDto?)null);
     
        //Act
        var result = _controller.GetAnswerKey(answerKeyId);
        
        //Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Gabarito não encontrado!", notFoundResult.Value);
    }
}