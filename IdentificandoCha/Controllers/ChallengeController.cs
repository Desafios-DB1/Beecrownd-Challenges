using IdentificandoCha.DTOs;
using IdentificandoCha.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentificandoCha.Controllers;

[ApiController]
[Route("[controller]")]
public class ChallengeController(IChallengeServices challengeService)
    : ControllerBase
{
    [HttpGet]
    public IActionResult GetChallenge()
    {
        return Ok("Bem-vindo ao desafio IDENTIFIQUE O CHÁ! Esse desafio consiste nos participantes" +
            "tentarem adivinhar o sabor do chá pelo cheiro, sabor, e textura! Cada acerto vale 100 pontos, e quem" +
            "possuir mais pontos no final é o vencedor! Quando finalizar o cadastro dos partipantes em /contestant " +
            "faça o post para o endpoint /challenge para informar as respostas e atualizar os pontos!");
    }

    [HttpPost("{challengeId:int}/answers")]
    public IActionResult PostChallengeAnswers(int challengeId, [FromBody] List<ContestantAnswer> answers)
    {
        var validation = challengeService.ValidateAnswers(answers);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors.First().ErrorMessage);
        }

        var request = (challengeId, answers);
        
        challengeService.CheckAnswers(request);
        
        return Ok("Respostas enviadas com sucesso!");
    }
}