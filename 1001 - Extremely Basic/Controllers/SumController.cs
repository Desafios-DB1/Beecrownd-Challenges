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
    
    
    [HttpGet]
    public string Get()
    {
        return "Teste 2";
    }
}