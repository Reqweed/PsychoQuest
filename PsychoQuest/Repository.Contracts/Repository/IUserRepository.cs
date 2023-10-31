using Entities.Models;

namespace Repository.Contracts.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken);

    Task<User> GetUserAsync(long userId, CancellationToken cancellationToken);

    Task DeleteUserAsync(long userId, CancellationToken cancellationToken);

    Task<bool> UserExistsAsync(long userId, CancellationToken cancellationToken);
    
    Task<bool> UserExistsAsync(string userEmail, CancellationToken cancellationToken);
}