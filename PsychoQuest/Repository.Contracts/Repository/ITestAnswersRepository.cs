using Entities.Enums;
using Entities.Models;

namespace Repository.Contracts.Repository;

public interface ITestAnswersRepository
{
    Task<TestAnswers> GetTestAnswersAsync(long userId, TypeTest typeTest);
    
    Task SaveTestAnswersAsync(TestAnswers testAnswers);
    
    Task DeleteTestAnswersAsync(long userId, TypeTest typeTest);
    
    Task<bool> TestAnswersExistsAsync(long userId, TypeTest typeTest);
}