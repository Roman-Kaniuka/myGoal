using Microsoft.AspNetCore.Mvc;
using myGoal.Domain.Dto;
using myGoal.Domain.Dto.User;
using myGoal.Domain.Interfaces.Services;
using myGoal.Domain.Result;

namespace myGoal.Api.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    //POST
    /// <summary>
    /// Реєстрація користувача
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody] RegisterUserDto dto)
    {
        var response = await _authService.Register(dto);
        if (response.IsSeccess)
        {
            return Ok();
        }

        return BadRequest();
    }
    /// <summary>
    /// Авторизація користувача
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    //POST
    [HttpPost("login")]
    public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody] LoginUserDto dto)
    {
        return BadRequest();
    }
}