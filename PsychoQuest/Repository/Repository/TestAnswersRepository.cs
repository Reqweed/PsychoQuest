using Entities.Enums;
using Entities.Models;
using MongoDB.Driver;
using Repository.Contracts.Repository;
using Repository.DbContext;

namespace Repository.Repository;

public class TestAnswersRepository : ITestAnswersRepository
{
    private readonly MongoDbContext _mongoDbContext;

    public TestAnswersRepository(MongoDbContext mongoDbContext) => _mongoDbContext = mongoDbContext;

    public async Task<TestAnswers> GetTestAnswersAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken)
    {
        var filter = Builders<TestAnswers>.Filter.And(
            Builders<TestAnswers>.Filter.Eq(nameof(TestAnswers.TestName), typeTest),
                      Builders<TestAnswers>.Filter.Eq(nameof(TestAnswers.UserId), userId)
        );
        
        var answers = await _mongoDbContext.TestAnswers.Find(filter).ToListAsync(cancellationToken);
        
        return answers.FirstOrDefault();
    }

    public async Task SaveTestAnswersAsync(TestAnswers testAnswers, CancellationToken cancellationToken)
    {
       await _mongoDbContext.TestAnswers.InsertOneAsync(testAnswers, cancellationToken: cancellationToken);
    }

    public async Task DeleteTestAnswersAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken)
    {
        var filter = Builders<TestAnswers>.Filter.And(
            Builders<TestAnswers>.Filter.Eq(nameof(TestAnswers.TestName), typeTest),
                      Builders<TestAnswers>.Filter.Eq(nameof(TestAnswers.UserId), userId)
        );

        await _mongoDbContext.TestAnswers.DeleteOneAsync(filter, cancellationToken: cancellationToken);
    }

    public async Task<bool> TestAnswersExistsAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken)
    {
        return await GetTestAnswersAsync(userId, typeTest, cancellationToken) is null;
    }
}