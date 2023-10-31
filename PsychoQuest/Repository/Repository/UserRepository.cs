using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts.Repository;
using Repository.DbContext;

namespace Repository.Repository;

public class UserRepository : IUserRepository
{
    private readonly PostgreDbContext _postgreDbContext;

    public UserRepository(PostgreDbContext postgreDbContext) => _postgreDbContext = postgreDbContext;

    public async Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await _postgreDbContext.Users.ToListAsync(cancellationToken);

        return users;
    }

    public async Task<User> GetUserAsync(long userId, CancellationToken cancellationToken)
    {
        var user = await _postgreDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        return user;
    }

    public async Task DeleteUserAsync(long userId, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(userId, cancellationToken);
        
        _postgreDbContext.Users.Remove(user);
        await _postgreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UserExistsAsync(long userId, CancellationToken cancellationToken)
    {
        return await GetUserAsync(userId, cancellationToken) is null;
    }

    public async Task<bool> UserExistsAsync(string userEmail, CancellationToken cancellationToken)
    {
        return await _postgreDbContext.Users
            .FirstOrDefaultAsync(u => u.Email == userEmail, cancellationToken) is null;
    }
}