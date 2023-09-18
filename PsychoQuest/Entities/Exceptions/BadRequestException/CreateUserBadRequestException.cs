namespace Entities.Exceptions.BadRequestException;

public class CreateUserBadRequestException : BadRequestException
{
    public CreateUserBadRequestException() : base("Error creating user.")
    {
    }
}