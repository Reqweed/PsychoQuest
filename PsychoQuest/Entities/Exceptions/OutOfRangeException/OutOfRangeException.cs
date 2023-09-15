namespace Entities.Exceptions.OutOfRangeException;

public abstract class OutOfRangeException : Exception
{
    protected OutOfRangeException(string message) : base(message)
    {
    }
}