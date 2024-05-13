using myGoal.Domain.Dto;
using myGoal.Domain.Dto.User;
using myGoal.Domain.Result;

namespace myGoal.Domain.Interfaces.Services;

/// <summary>
/// Сервіс призначений для реєстрації/авторизації
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Реєстрація користувача
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserDto>> Register(RegisterUserDto dto);
    
    /// <summary>
    /// Авторизація користувача
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
}