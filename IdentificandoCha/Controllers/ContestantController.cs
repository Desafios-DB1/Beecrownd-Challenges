using FluentValidation;
using IdentificandoCha.DTOs;
using IdentificandoCha.Exceptions;
using IdentificandoCha.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentificandoCha.Controllers;

[ApiController]
[Route("[controller]")]
public class ContestantController(IValidator<ContestantData> validator, ContestantService contestantService) : ControllerBase
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
        if (contestants?.Count == 0 || contestants == null)
        {
            return NoContent();
        }
        return Ok(contestants);
    }
}