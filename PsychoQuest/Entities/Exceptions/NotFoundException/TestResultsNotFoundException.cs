using Entities.Enums;

namespace Entities.Exceptions.NotFoundException;

public class TestResultsNotFoundException : NotFoundException
{
    public TestResultsNotFoundException(long userId, TypeTest typeTest) : base($"User with id: {userId} doesn't have result for test: {typeTest} in the database.")
    {
    }
}