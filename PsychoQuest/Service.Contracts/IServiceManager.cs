using Service.Contracts.Services;

namespace Service.Contracts;

public interface IServiceManager
{
    IScaleBeckCalculationService ScaleBeck { get; }
    
    ITestHallCalculationService TestHall { get; }
}