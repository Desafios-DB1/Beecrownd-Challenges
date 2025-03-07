using LeituraOtica.Interfaces.Services;

namespace LeituraOtica.Services;

public class OpticalConversionService : IOpticalConversionService
{
    private readonly char[] _letters = ['A', 'B', 'C', 'D', 'E'];
    public Dictionary<int, char> ConvertNumbersToLetters(List<int[]> answers)
    {
        var result = answers.Select((answer, index) =>
            {
                if (MoreThanOneValueBelow127(answer))
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

    private static bool MoreThanOneValueBelow127(int[] values)
    {
        return values.Count(value => value <= 127) > 1;
    }

    private static int GetMinValueIndex(int[] values)
    {
        return Array.IndexOf(values, values.Min());
    }
}