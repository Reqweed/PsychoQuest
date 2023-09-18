namespace Entities.Exceptions.BadRequestException;

public class DataUserBadRequestException : BadRequestException
{
    public DataUserBadRequestException(string field) : base($"Invalid {field}.")
    {
    }
}