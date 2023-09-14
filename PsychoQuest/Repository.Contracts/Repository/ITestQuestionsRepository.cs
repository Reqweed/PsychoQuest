using Entities.Enums;
using Entities.Models;

namespace Repository.Contracts.Repository;

public interface ITestQuestionsRepository
{
    Task<TestQuestions> GetTestQuestionsAsync(TypeTest typeTest);
}