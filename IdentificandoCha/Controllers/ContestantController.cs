using IdentificandoCha.DTOs;
using IdentificandoCha.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentificandoCha.Controllers;

[ApiController]
[Route("[controller]")]
public class ContestantController() : ControllerBase
{
    [HttpPost]
    public IActionResult RegisterContestants([FromBody] ContestantData contestant)
    {
        if (string.IsNullOrEmpty(contestant.Name)) {return BadRequest(new ResponseMessage("Forneça um nome para o competidor!"));}
        var newContestant = ContestantService.AddContestant(contestant);
        return CreatedAtAction(nameof(GetContestants), $"{newContestant.Name} foi registrado com sucesso! Seu número é {newContestant.Id}");
    }

    [HttpGet]
    public IActionResult GetContestants()
    {
        var contestants = ContestantService.GetAllContestants();
        if (contestants?.Count == 0 || contestants == null)
        {
            return NoContent();
        }
        return Ok(contestants);
    }
}