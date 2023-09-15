using Entities.Models;

namespace Service.Contracts.Services;

public interface ITestHallCalculationService
{
    TestResults CalculateForTestHall(TestAnswers testAnswers);
}