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
    }
}