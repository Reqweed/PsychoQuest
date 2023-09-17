using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts.Repository;
using Repository.DbContext;

namespace Repository.Repository;

public class UserRepository : IUserRepository
{
    private readonly PostgreDbContext _postgreDbContext;

    public UserRepository(PostgreDbContext postgreDbContext) => _postgreDbContext = postgreDbContext;

    public IEnumerable<User> GetAllUsers()
    {
        var users = _postgreDbContext.Users;

        return users;
    }

    public async Task<User> GetUserAsync(long userId)
    {
        var user = await _postgreDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        return user;
    }

    public async Task DeleteUserAsync(long userId)
    {
        var user = await GetUserAsync(userId);
        
        _postgreDbContext.Users.Remove(user);
        await _postgreDbContext.SaveChangesAsync();
    }

    public async Task<bool> UserExistsAsync(long userId)
    {
        return await GetUserAsync(userId) is null;
    }

    public async Task<bool> UserExistsAsync(string userEmail)
    {
        return await _postgreDbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail) is null;
    }
}