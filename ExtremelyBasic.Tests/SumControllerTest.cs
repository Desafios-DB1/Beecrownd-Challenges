using _1001___Extremely_Basic.Controllers;
using _1001___Extremely_Basic.DTOs;
using _1001___Extremely_Basic.Services;
using Microsoft.AspNetCore.Mvc;

namespace SumControllerTests;

public class SumControllerTest
{
    private readonly SumService _service;
    private readonly SumController _controller;

    public SumControllerTest()
    {
        _service = new SumService();
        _controller = new SumController(_service);
    }
    
    [Fact]
    public void PostNumbers_ReturnSuccessMessage_WhenStoreValues()
    {
        var request = new SumRequest { Number1 = 1, Number2 = 2 };
        
        var result = _controller.PostNumbers(request);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        
        var value = Assert.IsType<ResponseMessage>(okResult.Value);
        Assert.NotNull(value);
        Assert.Equal("Valores armazenados com sucesso!", value.Message);
    }

    [Fact]
    public void PostNumbers_ReturnBadRequest_WithNullRequest()
    {
        var request = new SumRequest { Number1 = null, Number2 = null };
        
        var result = _controller.PostNumbers(request);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var value = Assert.IsType<ResponseMessage>(badRequestResult.Value);
        Assert.Equal("Requisição inválida!", value.Message);
    }

    [Theory]
    [InlineData(-1, -30)]
    [InlineData(15, -30)]
    [InlineData(-13, 3)]
    public void PostNumbers_ReturnBadRequest_WithInvalidValues(int num1, int num2)
    {
        var request = new SumRequest { Number1 = num1, Number2 = num2 };
        
        var result = _controller.PostNumbers(request);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var value = Assert.IsType<ResponseMessage>(badRequestResult.Value);
        Assert.Equal("Requisição inválida!", value.Message);
    }

    [Theory]
    [InlineData(10, 30, 40)]
    [InlineData(34, 2, 36)]
    [InlineData(0, 0, 0)]
    public void GetSum_ReturnOk_WithStoredSum(int num1, int num2, int expectedSum)
    {
        var request = new SumRequest { Number1 = num1, Number2 = num2 };
        _controller.PostNumbers(request);
        var result = _controller.GetSum();
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var value = Assert.IsType<ResponseMessage>(okResult.Value);
        Assert.Equal($"A soma dos valores é: {expectedSum}", value.Message);
    }

    [Fact]
    public void GetSum_ReturnNotFound_WhenNoValueStored()
    {
        var result = _controller.GetSum();
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var value = Assert.IsType<ResponseMessage>(notFoundResult.Value);
        Assert.Equal("Nenhum valor armazenado ainda!", value.Message);
    }
}