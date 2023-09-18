namespace Entities.Exceptions.NotFoundException;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(long userId) : base($"Database doesn't have user with id: {userId}.")
    {
    }
}