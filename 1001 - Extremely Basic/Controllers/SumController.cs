using _1001___Extremely_Basic.DTOs;
using _1001___Extremely_Basic.Services;
using Microsoft.AspNetCore.Mvc;

namespace _1001___Extremely_Basic.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SumController : ControllerBase
{
    private static readonly SumService _sumService;

    public SumController(SumService sumService)
    {
        _sumService = sumService;
    }

    [HttpPost]
    public IActionResult PostNumbers([FromBody] SumRequest request)
    {
        _sumService.StoreValues(request);
        return Ok(new { message = "Valores armazenados com sucesso!" });
    }
    
    
}