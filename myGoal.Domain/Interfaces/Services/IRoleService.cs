using myGoal.Domain.Dto.Role;
using myGoal.Domain.Entity;
using myGoal.Domain.Result;

namespace myGoal.Domain.Interfaces.Services;

/// <summary>
/// Сервіс для керування ролями
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Створення нової ролі
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto);
    
    /// <summary>
    /// Видалення ролі
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<RoleDto>> DeleteRoleAsync(long id);
    
    /// <summary>
    /// Оновлення ролі
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<RoleDto>> UpdateRoleAsync(RoleDto dto);

    /// <summary>
    /// Присвоєння ролі користувачу
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto);

}