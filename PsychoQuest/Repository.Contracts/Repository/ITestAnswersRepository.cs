using Entities.Enums;
using Entities.Models;

namespace Repository.Contracts.Repository;

public interface ITestAnswersRepository
{
    Task<TestAnswers> GetTestAnswersAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken);
    
    Task SaveTestAnswersAsync(TestAnswers testAnswers, CancellationToken cancellationToken);
    
    Task DeleteTestAnswersAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken);
    
    Task<bool> TestAnswersExistsAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken);
}