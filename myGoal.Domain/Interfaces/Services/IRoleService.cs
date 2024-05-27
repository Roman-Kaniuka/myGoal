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
    Task<BaseResult<Role>> CreateRoleAsync(RoleDto dto);
    
    /// <summary>
    /// Видалення ролі
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<Role>> DeleteRoleAsync(long id);
    
    /// <summary>
    /// Оновлення ролі
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<Role>> UpdateRoleAsync(RoleDto dto);

}