using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeituraOtica.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentAnswerController(IStudentAnswerService studentAnswerService) : ControllerBase
{
    [HttpPost]
    public IActionResult AddStudentAnswer(StudentAnswerDto studentAnswer)
    {
        var result = studentAnswerService.AddStudentAnswer(studentAnswer);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        
        return Ok(result.Data);
    }

    [HttpGet]
    public IActionResult GetAllStudentsAnswers()
    {
        var result = studentAnswerService.GetAllStudentsAnswers();

        if (result is { Count: 0 }) return NoContent();
        
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetStudentAnswerById(int id)
    {
        var result = studentAnswerService.GetStudentAnswerById(id);
        
        if (result == null) return NotFound("Nenhuma resposta encontrada!");
        
        return Ok(result);
    }
}