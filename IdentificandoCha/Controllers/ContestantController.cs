using IdentificandoCha.DTOs;
using IdentificandoCha.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentificandoCha.Controllers;

[ApiController]
[Route("[controller]")]
public class ContestantController : ControllerBase
{
    private readonly ContestantService _contestantService;

    public ContestantController(ContestantService contestantService)
    {
        _contestantService = contestantService;
    }
    
    [HttpPost]
    public IActionResult RegisterContestants([FromBody] ContestantData contestant)
    {
        if (string.IsNullOrEmpty(contestant.Name)) {return BadRequest(new ResponseMessage("Forneça um nome para o competidor!"));}
        var newContestant = _contestantService.AddContestant(contestant);
        return CreatedAtAction(nameof(GetContestants), $"{newContestant.Name} foi registrado com sucesso! Seu número é {newContestant.Id}");
    }

    [HttpGet]
    public IActionResult GetContestants()
    {
        var contestants = _contestantService.GetAllContestants();
        return Ok(contestants);
    }
}