using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts.Repository;
using Repository.DbContext;

namespace Repository.Repository;

public class TestResultsRepository : ITestResultsRepository
{
    private readonly PostgreDbContext _postgreDbContext;

    public TestResultsRepository(PostgreDbContext postgreDbContext) => _postgreDbContext = postgreDbContext;
    
    public async Task<TestResults> GetTestResultsAsync(long userId, TypeTest typeTest)
    {
        var results = await _postgreDbContext.TestResults
            .FirstOrDefaultAsync(res => res.UserId == userId && res.TestName == typeTest);

        return results;
    }

    public async Task SaveTestResultsAsync(TestResults testResults)
    {
        await _postgreDbContext.TestResults.AddAsync(testResults);
        await _postgreDbContext.SaveChangesAsync();
    }

    public async Task DeleteTestResultsAsync(long userId, TypeTest typeTest)
    {
        var resultToDelete = await GetTestResultsAsync(userId,typeTest);
        
        _postgreDbContext.TestResults.Remove(resultToDelete);
        await _postgreDbContext.SaveChangesAsync();
    }

    public async Task<bool> TestResultExistsAsync(long userId, TypeTest typeTest)
    {
        return await GetTestResultsAsync(userId,typeTest) is null;
    }
}