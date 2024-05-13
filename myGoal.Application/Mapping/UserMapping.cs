using AutoMapper;
using myGoal.Domain.Dto.User;
using myGoal.Domain.Entity;

namespace myGoal.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping() 
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}