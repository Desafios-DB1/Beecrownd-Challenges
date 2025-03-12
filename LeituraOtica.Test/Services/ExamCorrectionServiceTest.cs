using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Services;
using Moq;

namespace LeituraOpticaTest.Services;

public class ExamCorrectionServiceTest
{
    private readonly Mock<IAnswerKeyService> _mockAnswerKeyService;
    private readonly Mock<IExamService> _mockExamService;
    private readonly ExamCorrectionService _service;

    public ExamCorrectionServiceTest()
    {
        _mockAnswerKeyService = new Mock<IAnswerKeyService>();
        _mockExamService = new Mock<IExamService>();
        _service = new ExamCorrectionService(_mockAnswerKeyService.Object, _mockExamService.Object);
    }

    [Fact]
    public void Correction_ReturnsFinalGrade_WhenValid()
    {
        var examId = Guid.NewGuid();
        var answerKey = new AnswerKeyDto(examId, new Dictionary<int, char>
        {
            { 1, 'A' }
        });
        var studentAnswers = new StudentAnswerWithGradeDto(examId, answerKey.Id, new Dictionary<int, char>
        {
            { 1, 'A' }
        });
        _mockExamService.Setup(x=>x.ExamExists(examId)).Returns(true);
        _mockAnswerKeyService.Setup(x => x.GetAnswerKeyAnswers(answerKey.Id)).Returns(answerKey.Answers);
        _mockExamService.Setup(x => x.GetExamValue(examId)).Returns(10);
        _mockAnswerKeyService.Setup(x => x.GetTotalQuestions(answerKey.Id)).Returns(1);

        var result = _service.Correction(studentAnswers);
        
        Assert.Equal(10, result);
    }

    [Fact]
    public void Correction_Return0_WhenExamDoesNotExist()
    {
        var examId = Guid.NewGuid();
        var answerKeyId = Guid.NewGuid();
        var studentAnswers = new StudentAnswerWithGradeDto(examId, answerKeyId, new Dictionary<int, char>());
        _mockExamService.Setup(x=>x.ExamExists(examId)).Returns(false);
        
        var result = _service.Correction(studentAnswers);
        
        Assert.Equal(0, result);
    }

    [Fact]
    public void Correction_Return0_WhenAnswerKeyDoesNotExist()
    {
        var examId = Guid.NewGuid();
        var answerKeyId = Guid.NewGuid();
        var studentAnswers = new StudentAnswerWithGradeDto(examId, answerKeyId, new Dictionary<int, char>());
        _mockExamService.Setup(x=>x.ExamExists(examId)).Returns(true);
        _mockAnswerKeyService.Setup(x => x.GetAnswerKeyAnswers(answerKeyId)).Returns((Dictionary<int, char>?)null);
        
        var result = _service.Correction(studentAnswers);
        
        Assert.Equal(0, result);
    }
}