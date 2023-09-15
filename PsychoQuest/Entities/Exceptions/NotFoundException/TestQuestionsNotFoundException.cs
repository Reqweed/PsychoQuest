using Entities.Enums;

namespace Entities.Exceptions.NotFoundException;

public class TestQuestionsNotFoundException : NotFoundException
{
    public TestQuestionsNotFoundException(TypeTest typeTest) : base($"Server doesn't have questions for test: {typeTest} in the database.")
    {
    }
}