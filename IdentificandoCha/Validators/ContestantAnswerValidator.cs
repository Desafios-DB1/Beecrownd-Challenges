using FluentValidation;
using IdentificandoCha.DTOs;
using IdentificandoCha.Interfaces.Services;
using IdentificandoCha.Services;

namespace IdentificandoCha.Validators;

public class ContestantAnswerValidator : AbstractValidator<List<ContestantAnswer>>
{
    public ContestantAnswerValidator(IContestantService contestantService)
    {
        RuleFor(x => x)
            .Must(answers => answers.Count == contestantService.GetAllContestants().Count)
            .WithMessage("Todos os participantes devem enviar somente uma resposta!");
    }
}