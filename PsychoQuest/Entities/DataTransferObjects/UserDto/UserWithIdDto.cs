using Entities.Enums;

namespace Entities.DataTransferObjects.UserDto;

public record UserWithIdDto(long Id,string UserName, string Email, Gender Gender);