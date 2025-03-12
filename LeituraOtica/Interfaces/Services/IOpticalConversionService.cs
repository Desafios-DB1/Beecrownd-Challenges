namespace LeituraOtica.Interfaces.Services;

public interface IOpticalConversionService
{
    Dictionary<int, char> ConvertNumbersToLetters(List<int[]> studentAnswersInNumbers);
}