using Entities.Models;
using Repository.Contracts;
using Service.Contracts;
using Service.Contracts.Services;
using Service.Services;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IScaleBeckCalculationService> _scaleBeckCalculationService;
    private readonly Lazy<ITestHallCalculationService> _testHallCalculationService;
    
    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _testHallCalculationService = new Lazy<ITestHallCalculationService>(() => new TestHallCalculationService(repositoryManager));
        _scaleBeckCalculationService = new Lazy<IScaleBeckCalculationService>(() => new ScaleBeckCalculationService(repositoryManager));
    }

    public IScaleBeckCalculationService ScaleBeck => _scaleBeckCalculationService.Value;
    public ITestHallCalculationService TestHall => _testHallCalculationService.Value;
    
    public TestResults Calculate(TestAnswers testAnswers)
    {
        var propertyName = testAnswers.TestName.ToString();
        var test = GetType().GetProperty(propertyName).GetValue(this);
        var methodInfo = test.GetType().GetMethod("CalculateTest");
        var result = methodInfo.Invoke(test, new object[] { testAnswers }) as TestResults;

        return result;
    }
}