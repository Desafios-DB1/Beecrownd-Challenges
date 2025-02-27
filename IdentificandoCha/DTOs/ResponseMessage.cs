namespace IdentificandoCha.DTOs;

public class ResponseMessage(string message)
{
    public string Message { get; } = message;
}