using FluentValidation;
using IdentificandoCha.DTOs;
using IdentificandoCha.Services;

namespace IdentificandoCha.Validators;

public class ContestantAnswerValidator : AbstractValidator<List<ContestantAnswer>>
{
    public ContestantAnswerValidator(ContestantService contestantService)
    {
        RuleFor(x => x)
            .Must(answers => answers.Count == ContestantService.GetAllContestants().Count)
            .WithMessage("Todos os participantes devem enviar somente uma resposta!");
    }
}