namespace IdentificandoCha.Exceptions;

public class BusinessException : Exception
{
    private List<string> Errors { get; }

    public BusinessException(List<string> errors)
        : base("Um ou mais erros de validação aconteceram!")
    {
        Errors = errors;
    }

    public BusinessException(string error)

        : base(error)
    {
        Errors = [error];
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, Errors);
    }
}