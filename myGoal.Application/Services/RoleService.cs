using Microsoft.EntityFrameworkCore;
using myGoal.Application.Resources;
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

    public RoleService(IBaseRepository<Role> roleRepository, IBaseRepository<User> userRepository)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async Task<BaseResult<Role>> CreateRoleAsync(RoleDto dto)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Name == dto.Name);
        if (role != null)
        {
            return new BaseResult<Role>()
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
        return new BaseResult<Role>()
        {
            Date = role
        };
    }

    public async Task<BaseResult<Role>> DeleteRoleAsync(long id)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Id == id);
        if (role ==null)
        {
            return new BaseResult<Role>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }
        
        await _roleRepository.RemoveAsync(role);
        return new BaseResult<Role>()
        {
            Date = role
        };
    }

    public async Task<BaseResult<Role>> UpdateRoleAsync(RoleDto dto)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Id == dto.Id);
        if (role == null)
        {
            return new BaseResult<Role>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }

        role.Name = dto.Name;
        await _roleRepository.UpdateAsync(role);
        return new BaseResult<Role>()
        {
            Date = role
        };
    }
}