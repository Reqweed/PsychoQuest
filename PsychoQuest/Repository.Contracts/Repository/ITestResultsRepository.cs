using Entities.Enums;
using Entities.Models;

namespace Repository.Contracts.Repository;

public interface ITestResultsRepository
{
    Task<TestResults> GetTestResultsAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken); 
    
    Task SaveTestResultsAsync(TestResults testResults, CancellationToken cancellationToken);
    
    Task DeleteTestResultsAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken);
    
    Task<bool> TestResultExistsAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken);
}