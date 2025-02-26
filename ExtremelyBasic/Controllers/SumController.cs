using _1001___Extremely_Basic.DTOs;
using _1001___Extremely_Basic.Services;
using Microsoft.AspNetCore.Mvc;

namespace _1001___Extremely_Basic.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SumController : ControllerBase
{
    private readonly SumService _sumService;

    public SumController(SumService sumService)
    {
        _sumService = sumService;
    }

    [HttpPost]
    public IActionResult PostNumbers([FromBody] SumRequest request)
    {
        if (request.Number1 == null || request.Number2 == null)
        {
            return BadRequest(new ResponseMessage("Requisição inválida!"));
        }
        
        try
        {
            _sumService.StoreValues(request);
            return Ok(new ResponseMessage("Valores armazenados com sucesso!"));
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseMessage("Erro interno!"));
        }
    }
    
    [HttpGet]
    public IActionResult GetSum()
    {
        try
        {
            var result = _sumService.GetSum();
            if (result == null) return NotFound(new ResponseMessage("Nenhum valor armazenado ainda!"));
            return Ok(new ResponseMessage($"A soma dos valores é: {result}"));
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseMessage("Erro interno!"));
        }
    }
}