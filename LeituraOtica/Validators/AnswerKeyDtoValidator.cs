using FluentValidation;
using LeituraOtica.Dtos;

namespace LeituraOtica.Validators;

public class AnswerKeyDtoValidator : AbstractValidator<AnswerKeyDto>
{
    public AnswerKeyDtoValidator()
    {
        RuleFor(x => x.ExamId)
            .NotNull().WithMessage("O id da prova é obrigatório!")
            .NotEmpty().WithMessage("O id da prova não pode estar vazio!");
        
        RuleFor(x => x.Answers)
            .NotNull().WithMessage("A lista de respostas é obrigatória!")
            .NotEmpty().WithMessage("A lista de respostas não pode estar vazia!");
    }
}