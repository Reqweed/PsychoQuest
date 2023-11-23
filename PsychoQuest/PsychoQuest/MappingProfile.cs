using AutoMapper;
using Entities.DataTransferObjects.UserDto;
using Entities.Models;

namespace PsychoQuest;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User,UserDto>();
        CreateMap<User,UserWithIdDto>();
    }
}