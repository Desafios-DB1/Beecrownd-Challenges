using Microsoft.AspNetCore.Mvc;

namespace _1000___Hello_World.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController
{
    [HttpGet]
    public string Get()
    {
        return "Hello World";
    }
}