using Entities.Enums;

namespace Entities.Exceptions.BadRequestException;

public class TestResultBadRequestException : BadRequestException
{
    public TestResultBadRequestException(long points, TypeTest typeTest) : base($"Test: {typeTest} doesn't have result with point = {points}.")
    {
    }
}