using Entities.Models;

namespace Service.Contracts.Services;

public interface IScaleBeckCalculationService
{
    TestResults CalculateForScaleBeck(TestAnswers testAnswers);
}