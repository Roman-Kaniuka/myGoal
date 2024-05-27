using AutoMapper;
using myGoal.Domain.Dto.Role;
using myGoal.Domain.Entity;

namespace myGoal.Application.Mapping;

public class RoleMapping : Profile
{
    public RoleMapping()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
    }
}