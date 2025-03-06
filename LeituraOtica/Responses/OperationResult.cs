namespace LeituraOtica.Responses;

public class OperationResult(bool isSuccess, string? errorMessage = null, object? data = null)
{
    public bool IsSuccess => isSuccess;
    public string? ErrorMessage => errorMessage;
    public object? Data => data;
    
    public static OperationResult Success(object? data = null)
    {
        return new OperationResult(true, null, data);
    }

    public static OperationResult Failure(string? errorMessage = null)
    {
        return new OperationResult(false, errorMessage, null);
    }
}