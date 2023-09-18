namespace Entities.Exceptions.BadRequestException;

public class TokenBadRequestException : BadRequestException
{
    public TokenBadRequestException() : base("Invalid token.")
    {
    }
}