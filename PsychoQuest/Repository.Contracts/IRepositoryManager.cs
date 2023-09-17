using Repository.Contracts.Repository;

namespace Repository.Contracts;

public interface IRepositoryManager
{
    ITestResultsRepository TestResults { get; }
    
    ITestAnswersRepository TestAnswers { get; }
    
    ITestQuestionsRepository TestQuestions { get; }
    
    IUserRepository User { get; }
}