using Entities.Enums;

namespace Entities.Exceptions.BadRequestException;

public class AnswerOptionBadRequestException : BadRequestException
{
    public AnswerOptionBadRequestException(int answer, TypeTest typeTest) : base($"Test: {typeTest} doesn't have answer options: {answer}.")
    {
    }
}