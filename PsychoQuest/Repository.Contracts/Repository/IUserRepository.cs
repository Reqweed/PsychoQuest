using Entities.Models;

namespace Repository.Contracts.Repository;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();

    Task<User> GetUserAsync(long userId);

    Task DeleteUserAsync(long userId);

    Task<bool> UserExistsAsync(long userId);
    
    Task<bool> UserExistsAsync(string userEmail);
}