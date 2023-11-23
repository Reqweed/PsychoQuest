using Entities.Enums;

namespace Entities.DataTransferObjects.UserDto;

public record UserDto(string UserName, string Email, Gender Gender);