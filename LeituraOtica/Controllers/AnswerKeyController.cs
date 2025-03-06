using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeituraOtica.Controllers;

[ApiController]
[Route("[controller]")]
public class AnswerKeyController (IAnswerKeyService answerKeyService) : ControllerBase
{
    [HttpPost]
    public IActionResult SaveAnswerKey(AnswerKeyDto answerKey)
    {
        var result = answerKeyService.SaveAnswerKey(answerKey);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        
        return Ok(result.Data);
    }

    [HttpGet]
    public IActionResult GetAnswerKeys()
    {
        var result = answerKeyService.GetAllAnswerKeys();

        if (result is { Count: 0 }) return NoContent();
        
        return Ok(result);
    }

    [HttpGet]
    [Route("{answerKeyId:int}")]
    public IActionResult GetAnswerKey(int answerKeyId)
    {
        var result = answerKeyService.GetAnswerKey(answerKeyId);
        if (result == null) return NotFound("Gabarito não encontrado!");
        return Ok(result);
    }
}