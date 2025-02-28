using FluentValidation;
using IdentificandoCha.DTOs;
using IdentificandoCha.Exceptions;
using IdentificandoCha.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentificandoCha.Controllers;

[ApiController]
[Route("[controller]")]
public class ContestantController(IValidator<ContestantData> validator, IContestantService contestantService) : ControllerBase
{
    [HttpPost]
    public IActionResult RegisterContestants([FromBody] ContestantData contestant)
    {
        var validationResult = validator.Validate(contestant);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new BusinessException(errors);
        }
        
        var newContestant = contestantService.AddContestant(contestant);
        return CreatedAtAction(nameof(GetContestants), $"{newContestant.Name} foi registrado com sucesso! Seu número é {newContestant.Id}");
    }

    [HttpGet]
    public IActionResult GetContestants()
    {
        var contestants = contestantService.GetAllContestants();
        return contestants.Count == 0 ? NoContent() : Ok(contestants);
    }
}