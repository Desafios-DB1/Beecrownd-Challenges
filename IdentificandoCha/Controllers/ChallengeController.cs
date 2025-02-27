using IdentificandoCha.DTOs;
using IdentificandoCha.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentificandoCha.Controllers;

[ApiController]
[Route("[controller]")]
public class ChallengeController(ChallengeService challengeService)
    : ControllerBase
{
    [HttpGet]
    public IActionResult GetChallenge()
    {
        return Ok(new ResponseMessage(
            "Bem-vindo ao desafio IDENTIFIQUE O CHÁ! Esse desafio consiste nos participantes" +
            "tentarem adivinhar o sabor do chá pelo cheiro, sabor, e textura! Cada acerto vale 100 pontos, e quem" +
            "possuir mais pontos no final é o vencedor! Quando finalizar o cadastro dos partipantes em /contestant " +
            "faça o post para o endpoint /challenge para informar as respostas e atualizar os pontos!"));
    }

    [HttpPost("{challengeId:int}/answers")]
    public IActionResult PostChallengeAnswers(int challengeId, [FromBody] List<ContestantAnswer> answers)
    {
        if (answers.Count < ContestantService.GetAllContestants()!.Count)
        {
            return BadRequest(new ResponseMessage("Todos os participantes devem enviar apenas uma resposta!"));
        }

        var request = new AnswersRequest()
        {
            ChallengeId = challengeId,
            Answers = answers,
        };
        challengeService.CheckAnswers(request);
        
        return Ok(new ResponseMessage("Respostas enviadas com sucesso!"));
    }
}