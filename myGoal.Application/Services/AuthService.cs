using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using myGoal.Application.Resources;
using myGoal.Domain.Dto;
using myGoal.Domain.Dto.User;
using myGoal.Domain.Entity;
using myGoal.Domain.Enum;
using myGoal.Domain.Interfaces.Repositories;
using myGoal.Domain.Interfaces.Services;
using myGoal.Domain.Result;
using Serilog;

namespace myGoal.Application.Services;

public class AuthService : IAuthService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<UserToken> _userTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    
    
    public AuthService(IBaseRepository<User> userRepository, ILogger logger, IMapper mapper, 
        IBaseRepository<UserToken> userTokenRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
        _userTokenRepository = userTokenRepository;
        _tokenService = tokenService;
    }

    public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
    {
        if (dto.Password != dto.PasswordConfirm)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm
            };
        }

        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Login == dto.Login);
            if (user!=null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.UserAlreadyExists,
                    ErrorCode = (int)ErrorCodes.UserAlreadyExists
                };
            }
            
            var hashUserPassword = HashPassword(dto.Password);
            user = new User()
            {
                Login = dto.Login,
                Password = hashUserPassword
            };
            
            await  _userRepository.CreateAsync(user);
            return new BaseResult<UserDto>()
            {
                Date =  _mapper.Map<UserDto>(user)
            };
        }
        
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm
            };
        }
    }

    public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Login == dto.Login);
            if (user == null)
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            if (!IsVerifyPassword(user.Password, dto.Password))
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.PasswordIsWrong,
                    ErrorCode = (int)ErrorCodes.PasswordIsWrong
                };
            }
            
            var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role,"user")

            };
            
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            
            if (userToken == null)
            {
                userToken = new UserToken()
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
                };
                await _userTokenRepository.CreateAsync(userToken);
            }
            
            else
            {
                userToken.RefreshToken = refreshToken;
                userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                await _userTokenRepository.UpdateAsync(userToken);
            }

            return new BaseResult<TokenDto>()
            {
                Date = new TokenDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                }
            };
        }
        
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }
    private string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
    private bool IsVerifyPassword(string userPasswordHash, string userPassword)
    {
        var hash = HashPassword(userPassword);
        return hash == userPasswordHash;
    }
}