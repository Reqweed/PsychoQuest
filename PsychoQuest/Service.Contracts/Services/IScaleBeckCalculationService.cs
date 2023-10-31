using Entities.Models;

namespace Service.Contracts.Services;

public interface IScaleBeckCalculationService
{
    TestResults CalculateTest(TestAnswers testAnswers);
}