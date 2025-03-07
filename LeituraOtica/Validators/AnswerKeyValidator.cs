using FluentValidation;
using LeituraOtica.Dtos;

namespace LeituraOtica.Validators;

public class AnswerKeyDtoValidator : AbstractValidator<AnswerKeyDto>
{
    public AnswerKeyDtoValidator()
    {
        RuleFor(x => x.ExamId)
            .NotEmpty().WithMessage("O id da prova não pode estar vazio!");
        
        RuleFor(x => x.Answers)
            .NotEmpty().WithMessage("A lista de respostas não pode estar vazia!");
    }
}