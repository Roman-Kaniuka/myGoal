using Microsoft.AspNetCore.Mvc;
using myGoal.Domain.Dto;
using myGoal.Domain.Interfaces.Services;
using myGoal.Domain.Result;

namespace myGoal.Api.Controllers;
/// <summary>
/// 
/// </summary>
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken([FromBody]TokenDto tokenDto)
    {
        var response = await _tokenService.RefreshToken(tokenDto);
        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);

    }
}