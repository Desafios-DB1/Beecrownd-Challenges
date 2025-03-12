using LeituraOtica.Interfaces.Services;

namespace LeituraOtica.Services;

public class OpticalConversionService : IOpticalConversionService
{
    private readonly char[] _letters = ['A', 'B', 'C', 'D', 'E'];
    public Dictionary<int, char> ConvertNumbersToLetters(List<int[]> studentAnswersInNumbers)
    {
        var result = studentAnswersInNumbers.Select((answer, index) =>
            {
                if (!OnlyOneValueBelow127(answer))
                {
                    return new KeyValuePair<int, char>(index + 1, '*');
                }

                var minValueIndex = GetMinValueIndex(answer);

                return new KeyValuePair<int, char>(
                    index + 1,
                    _letters[minValueIndex]);
            })
            .ToDictionary(x => x.Key, x => x.Value);

        return result;
    }

    private static bool OnlyOneValueBelow127(int[] values)
    {
        if (values.Count(value => value <= 127) > 1)
            return false;

        if (values.All(value=>value < 127))
            return false;
        
        return true;
    }

    private static int GetMinValueIndex(int[] values)
    {
        return Array.IndexOf(values, values.Min());
    }
}