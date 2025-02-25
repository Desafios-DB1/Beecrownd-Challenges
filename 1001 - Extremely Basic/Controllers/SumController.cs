using Microsoft.AspNetCore.Mvc;

namespace _1001___Extremely_Basic.Controllers;

[ApiController]
[Route("[controller]")]
public class SumController
{
    [HttpGet]
    public string Get()
    {
        return "Teste";
    }
}