using LeituraOtica.Interfaces.Services;

namespace LeituraOtica.Services;

public class OpticalConversionService : IOpticalConversionService
{
    private readonly char[] _answers = ['A', 'B', 'C', 'D', 'E'];
    public Dictionary<int, char>? ConvertAnswersToLetters(List<int[]> answers)
    {
        var result = answers.Select((answer, index) =>
            {
                var countBelow127 = answer.Count(value => value <= 127);

                if (countBelow127 > 1)
                {
                    return new KeyValuePair<int, char>(index + 1, '*');
                }
                
                var minValueIndex = Array.IndexOf(answer, answer.Min());

                return new KeyValuePair<int, char>(
                    index + 1,
                    minValueIndex >= 0 && minValueIndex < _answers.Length
                        ? _answers[minValueIndex]
                        : '*');
            })
            .ToDictionary(x => x.Key, x => x.Value);
        
        return result;
    }
}