using Entities.Enums;

namespace Entities.Exceptions.OutOfRangeException;

public class AnswerOptionOutOfRangeException : OutOfRangeException
{
    public AnswerOptionOutOfRangeException(int answer, TypeTest typeTest) : base($"Test: {typeTest} doesn't have answer options: {answer}.")
    {
    }
}