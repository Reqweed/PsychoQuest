namespace Entities.Exceptions.BadRequestException;

public class CreateRoleBadRequestException : BadRequestException
{
    public CreateRoleBadRequestException() : base("Error creating role.")
    {
    }
}