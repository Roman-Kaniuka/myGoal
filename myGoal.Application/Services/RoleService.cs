using AutoMapper;
using Microsoft.EntityFrameworkCore;
using myGoal.Application.Resources;
using myGoal.Domain.Dto.Report;
using myGoal.Domain.Dto.Role;
using myGoal.Domain.Entity;
using myGoal.Domain.Enum;
using myGoal.Domain.Interfaces.Repositories;
using myGoal.Domain.Interfaces.Services;
using myGoal.Domain.Result;

namespace myGoal.Application.Services;

public class RoleService : IRoleService
{
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public RoleService(IBaseRepository<Role> roleRepository, IBaseRepository<User> userRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Name == dto.Name);
        if (role != null)
        {
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleAlreadyExists,
                ErrorCode = (int)ErrorCodes.RoleAlreadyExists
            };
        }

        role = new Role()
        {
            Name = dto.Name
        };
        
        await _roleRepository.CreateAsync(role);
        return new BaseResult<RoleDto>()
        {
            Date = _mapper.Map<RoleDto>(role)
        };
    }

    public async Task<BaseResult<RoleDto>> DeleteRoleAsync(long id)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Id == id);
        if (role ==null)
        {
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }
        
        await _roleRepository.RemoveAsync(role);
        return new BaseResult<RoleDto>()
        {
            Date = _mapper.Map<RoleDto>(role)
        };
    }

    public async Task<BaseResult<RoleDto>> UpdateRoleAsync(RoleDto dto)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Id == dto.Id);
        if (role == null)
        {
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }

        role.Name = dto.Name;
        await _roleRepository.UpdateAsync(role);
        return new BaseResult<RoleDto>()
        {
            Date = _mapper.Map<RoleDto>(role)
        };
    }
}