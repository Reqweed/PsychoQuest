using Entities.Enums;

namespace Entities.Exceptions.OutOfRangeException;

public class TestResultOutOfRangeException : OutOfRangeException
{
    public TestResultOutOfRangeException(long points, TypeTest typeTest) : base($"Test: {typeTest} doesn't have result with point = {points}.")
    {
    }
}