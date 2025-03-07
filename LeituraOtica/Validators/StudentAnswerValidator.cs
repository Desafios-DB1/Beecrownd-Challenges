using FluentValidation;
using LeituraOtica.Dtos;

namespace LeituraOtica.Validators;

public class StudentAnswerDtoValidator : AbstractValidator<StudentAnswerDto>
{
    public StudentAnswerDtoValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("O id do estudante é obrigatorio");
        
        RuleFor(x => x.ExamId)
            .NotEmpty().WithMessage("O id da prova é obrigatorio");

        RuleFor(x => x.AnswerKeyId)
            .NotEmpty().WithMessage("O id do gabarito é obrigatório!");
        
        RuleFor(x => x.Answers)
            .NotEmpty().WithMessage("A lista de respostas é obrigatoria");

        RuleForEach(x => x.Answers)
            .Must(answ => answ is { Length: 5 })
            .WithMessage("Cada resposta deve conter exatamente 5 valores!");
        
        RuleForEach(x => x.Answers)
            .Must(answ => answ.All(v => v is >= 0 and <= 255))
            .WithMessage("Cada resposta deve estar entre 0 e 255!");
    }
}