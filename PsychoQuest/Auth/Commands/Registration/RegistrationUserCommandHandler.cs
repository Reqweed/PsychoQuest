﻿using Auth.Interfaces;
using Entities.Exceptions.BadRequestException;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Auth.Commands.Registration;

public class RegistrationUserCommandHandler : IRequestHandler<RegistrationUserCommand,AuthenticatedResponse>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<User> _userManager;
    private readonly ILoggerManager _loggerManager;

    public RegistrationUserCommandHandler(IRepositoryManager repositoryManager, IJwtGenerator jwtGenerator, UserManager<User> userManager, ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
        _loggerManager = loggerManager;
    }

    public async Task<AuthenticatedResponse> Handle(RegistrationUserCommand request, CancellationToken cancellationToken)
    {
        _loggerManager.LogInfo($"Command:RefreshTokenCommand - New user has begun");

        if (!await _repositoryManager.User.UserExistsAsync(request.Email, cancellationToken))
        {
            _loggerManager.LogWarn($"Command:RefreshTokenCommand - New user has existing mail");

            throw new DataUserBadRequestException("email has already been created");
        }
        
        var refreshToken = _jwtGenerator.CreateRefreshToken();

        var user = new User()
        {
            UserName = request.UserName,
            Email = request.Email,
            Gender = request.Gender,
            RefreshToken = refreshToken,
            BirthDate = request.BirthDate,
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            _loggerManager.LogWarn($"Command:RefreshTokenCommand - New User wasn't created successfully");

            throw new CreateUserBadRequestException();
        } 
        
        result = await _userManager.AddToRoleAsync(user, "User");

        if (!result.Succeeded)
        {
            _loggerManager.LogWarn($"Command:RefreshTokenCommand - New user wasn't add role successfully");

            throw new SetRoleBadRequestException(user.Id,"User");
        } 
        
        _loggerManager.LogInfo($"Command:RefreshTokenCommand - New user was finished");

        return new AuthenticatedResponse()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Gender = user.Gender,
            BirthDate = user.BirthDate,
            Token = _jwtGenerator.CreateToken(user),
            RefreshToken = user.RefreshToken
        };
    }
}