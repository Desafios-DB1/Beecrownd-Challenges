namespace IdentificandoCha.DTOs;

public class ResponseMessage(string message)
{
    public string Message { get; } = message;

    public static implicit operator ResponseMessage(string message)
    {
        return new ResponseMessage(message);
    }
}