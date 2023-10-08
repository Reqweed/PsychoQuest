namespace Entities.Exceptions.NotFoundException;

public class RoleNotFoundException : NotFoundException
{
    public RoleNotFoundException(long roleId) : base($"Database doesn't have role with id: {roleId}.")
    {
    }
    
    public RoleNotFoundException() : base($"Database doesn't have roles.")
    {
    }
}