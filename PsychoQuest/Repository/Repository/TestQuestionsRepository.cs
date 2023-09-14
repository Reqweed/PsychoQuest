using Entities.Enums;
using Entities.Models;
using MongoDB.Driver;
using Repository.Contracts.Repository;
using Repository.DbContext;

namespace Repository.Repository;

public class TestQuestionsRepository : ITestQuestionsRepository
{
    private readonly MongoDbContext _mongoDbContext;

    public TestQuestionsRepository(MongoDbContext mongoDbContext) => _mongoDbContext = mongoDbContext;

    public async Task<TestQuestions> GetTestQuestionsAsync(TypeTest typeTest)
    {
        var filter = Builders<TestQuestions>.Filter.And(
            Builders<TestQuestions>.Filter.Eq(nameof(TestQuestions.TestName), typeTest)
        );
        
        var questions = await _mongoDbContext.TestQuestions.Find(filter).FirstOrDefaultAsync();// fix async

        return questions;
    }
}