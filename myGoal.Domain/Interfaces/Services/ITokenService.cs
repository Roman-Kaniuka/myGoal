using System.Security.Claims;
using myGoal.Domain.Dto;
using myGoal.Domain.Result;

namespace myGoal.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
    
    Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
}