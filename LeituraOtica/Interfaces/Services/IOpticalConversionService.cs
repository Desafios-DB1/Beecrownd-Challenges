namespace LeituraOtica.Interfaces.Services;

public interface IOpticalConversionService
{
    Dictionary<int, char>? ConvertAnswersToLetters(List<int[]> answers);
}