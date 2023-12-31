﻿using Entities.Enums;
using Entities.Exceptions.BadRequestException;
using Entities.Models;
using Repository.Contracts;
using Service.Contracts.Services;

namespace Service.Services;

public class ScaleBeckCalculationService : IScaleBeckCalculationService
{
    private readonly IRepositoryManager _repositoryManager;

    public ScaleBeckCalculationService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public TestResults CalculateTest(TestAnswers testAnswers)
    {
        var testResult = new TestResults() { TestName = testAnswers.TestName, UserId = testAnswers.UserId };
        
        foreach (var answer in testAnswers.Answers)
        {
            testResult.Points += answer switch
            {
                1 => 0,
                2 => 1,
                3 => 2,
                4 => 3,
                _ => throw new AnswerOptionBadRequestException(answer, TypeTest.ScaleBeck)
            };
        }
        testResult.Result = testResult.Points switch
        {
            >= 0 and <= 9 => "Absence of depressive symptoms",
            > 9 and <= 15 => "Mild depression",
            > 15 and <= 19 => "Moderate depression",
            > 19 and <= 29 => "Severe depression(moderate)",
            > 29 and <= 63 => "Severe depression",
            _ => throw new TestResultBadRequestException(testResult.Points, TypeTest.ScaleBeck)
        };

        return testResult;
    }
}