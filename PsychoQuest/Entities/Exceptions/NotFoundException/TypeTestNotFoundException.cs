using Entities.Enums;

namespace Entities.Exceptions.NotFoundException;

public class TypeTestNotFoundException : NotFoundException
{
    public TypeTestNotFoundException(TypeTest typeTest) : base($"Server doesn't have typeTest {typeTest}")
    {
    }
}