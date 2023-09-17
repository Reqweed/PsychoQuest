using Repository.Contracts;
using Repository.Contracts.Repository;
using Repository.DbContext;
using Repository.Repository;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly MongoDbContext _mongoDbContext;
    private readonly PostgreDbContext _postgreDbContext;
    private readonly Lazy<ITestAnswersRepository> _testAnswers;
    private readonly Lazy<ITestResultsRepository> _testResults;
    private readonly Lazy<ITestQuestionsRepository> _testQuestions;
    private readonly Lazy<IUserRepository> _user;
    public RepositoryManager(MongoDbContext mongoDbContext, PostgreDbContext postgreDbContext)
    {
        _mongoDbContext = mongoDbContext;
        _postgreDbContext = postgreDbContext;
        _testAnswers = new Lazy<ITestAnswersRepository>(() => new TestAnswersRepository(_mongoDbContext));
        _testResults = new Lazy<ITestResultsRepository>(() => new TestResultsRepository(_postgreDbContext));
        _testQuestions = new Lazy<ITestQuestionsRepository>(() => new TestQuestionsRepository(_mongoDbContext));
        _user = new Lazy<IUserRepository>(() => new UserRepository(_postgreDbContext));
    }

    public ITestResultsRepository TestResults => _testResults.Value;
    public ITestAnswersRepository TestAnswers => _testAnswers.Value;
    public ITestQuestionsRepository TestQuestions => _testQuestions.Value;
    public IUserRepository User => _user.Value;
}