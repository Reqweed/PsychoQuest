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
    
    public async Task<TestResults> GetTestResultsAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken)
    {
        var results = await _postgreDbContext.TestResults
            .FirstOrDefaultAsync(res => res.UserId == userId && res.TestName == typeTest, cancellationToken);

        return results;
    }

    public async Task SaveTestResultsAsync(TestResults testResults, CancellationToken cancellationToken)
    {
        await _postgreDbContext.TestResults.AddAsync(testResults, cancellationToken);
        await _postgreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTestResultsAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken)
    {
        var resultToDelete = await GetTestResultsAsync(userId, typeTest, cancellationToken);
        
        _postgreDbContext.TestResults.Remove(resultToDelete);
        await _postgreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> TestResultExistsAsync(long userId, TypeTest typeTest, CancellationToken cancellationToken)
    {
        return await GetTestResultsAsync(userId, typeTest, cancellationToken) is null;
    }
}