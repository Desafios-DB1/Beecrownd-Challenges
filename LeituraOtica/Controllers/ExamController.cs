using FluentValidation;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeituraOtica.Controllers;

[ApiController]
[Route("[controller]")]
public class ExamController(IExamService examService) : ControllerBase
{
    [HttpPost]
    public IActionResult AddExam(ExamDto exam)
    {
        var result = examService.AddExam(exam);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Data);
    }

    [HttpGet]
    public IActionResult GetExams()
    {
        var result = examService.GetAllExams();
        if (result is { Count: 0 }) return NoContent();
        return Ok(result);
    }

    [HttpGet]
    [Route("{examId:int}")]
    public IActionResult GetExam(int examId)
    {
        var result = examService.GetExam(examId);
        if (result == null) return NotFound("Prova não encontrada!");
        return Ok(result);
    }
}