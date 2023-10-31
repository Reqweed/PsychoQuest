using Entities.Models;

namespace Service.Contracts.Services;

public interface ITestHallCalculationService
{
    TestResults CalculateTest(TestAnswers testAnswers);
}