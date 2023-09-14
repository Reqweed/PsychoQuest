using Entities.Enums;
using Entities.Models;

namespace Repository.Contracts.Repository;

public interface ITestResultsRepository
{
    Task<TestResults> GetTestResultsAsync(long userId, TypeTest typeTest); 
    
    Task SaveTestResultsAsync(TestResults testResults);
    
    Task DeleteTestResultsAsync(long userId, TypeTest typeTest);
    
    Task<bool> TestResultExistsAsync(long userId, TypeTest typeTest);
}