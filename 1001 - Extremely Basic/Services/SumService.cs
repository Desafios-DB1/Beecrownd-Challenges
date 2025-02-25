using _1001___Extremely_Basic.DTOs;

namespace _1001___Extremely_Basic.Services;

public class SumService
{
    private SumRequest? _currentValues;

    public void StoreValues(SumRequest values)
    {
        _currentValues = values;
    }

    public int? GetSum()
    {
        if (_currentValues == null) return null;
        return _currentValues.Number1 + _currentValues.Number2;
    }
}