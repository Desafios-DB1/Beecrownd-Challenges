using LeituraOtica.Interfaces.Services;
using LeituraOtica.Services;
using Moq;

namespace LeituraOpticaTest.Services;

public class ExamCorrectionServiceTest
{
    private readonly Mock<IAnswerKeyService> _mockService = new();
    private readonly Mock<IExamService> _mockExamService = new();
    private ExamCorrectionService _service;

    public ExamCorrectionServiceTest()
    {
        _service = new ExamCorrectionService(_mockService.Object, _mockExamService.Object);
    }
}