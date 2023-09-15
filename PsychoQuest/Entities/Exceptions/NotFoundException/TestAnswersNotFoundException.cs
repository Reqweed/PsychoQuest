using Entities.Enums;

namespace Entities.Exceptions.NotFoundException;

public class TestAnswersNotFoundException : NotFoundException
{
    public TestAnswersNotFoundException(long userId, TypeTest typeTest) : base($"User with id: {userId} doesn't have answers for test: {typeTest} in the database.")
    {
    }
}