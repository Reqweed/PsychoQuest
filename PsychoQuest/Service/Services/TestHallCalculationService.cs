using Entities.Enums;
using Entities.Exceptions.BadRequestException;
using Entities.Models;
using Repository.Contracts;
using Service.Contracts.Services;

namespace Service.Services;

public class TestHallCalculationService : ITestHallCalculationService
{
    private readonly IRepositoryManager _repositoryManager;

    public TestHallCalculationService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public TestResults CalculateForTestHall(TestAnswers testAnswers)
    {
        var testResult = new TestResults() { TestName = testAnswers.TestName, UserId = testAnswers.UserId };
        
        foreach (var answer in testAnswers.Answers)
        {
            testResult.Points += answer switch
            {
                1 => -3,
                2 => -2,
                3 => -1,
                4 => 1,
                5 => 2,
                6 => 3,
                _ => throw new AnswerOptionBadRequestException(answer, TypeTest.TestHall)
            };
        }
        testResult.Result = testResult.Points switch
        {
            <= 39 => "Low emotional intelligence",
            > 39 and <= 69 => "Medium emotional intelligence",
            > 69 and <=90 => "High emotional intelligence",
            _ => throw new TestResultBadRequestException(testResult.Points, TypeTest.TestHall)
        };
        
        return testResult;
    }
}