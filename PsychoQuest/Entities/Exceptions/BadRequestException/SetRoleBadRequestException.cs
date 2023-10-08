namespace Entities.Exceptions.BadRequestException;

public class SetRoleBadRequestException : BadRequestException
{
    public SetRoleBadRequestException(long userId,long roleId) : base($"Role with id:{roleId} wasn't added for user with id:{userId}")
    {
    }
    
    public SetRoleBadRequestException(long userId,string roleName) : base($"Role:{roleName} wasn't added for user with id:{userId}")
    {
    }
}