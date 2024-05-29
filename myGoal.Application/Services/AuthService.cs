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
using myGoal.Domain.Interfaces.Repositories.DataBases;
using myGoal.Domain.Interfaces.Services;
using myGoal.Domain.Result;
using Serilog;

namespace myGoal.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<UserToken> _userTokenRepository;
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    
    
    public AuthService(IBaseRepository<User> userRepository, ILogger logger, IMapper mapper, 
        IBaseRepository<UserToken> userTokenRepository, ITokenService tokenService, IBaseRepository<Role> roleRepository, IBaseRepository<UserRole> userRoleRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
        _userTokenRepository = userTokenRepository;
        _tokenService = tokenService;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
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
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    user = new User()
                    {
                        Login = dto.Login,
                        Password = hashUserPassword
                    };
                    await _unitOfWork.Users.CreateAsync(user);
                    await _unitOfWork.SaveChangesAsync();
                    
                    var role = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Name == (nameof(Roles.User)));
                    if (role == null)
                    {
                        return new BaseResult<UserDto>()
                        {
                            ErrorMessage = ErrorMessage.RoleNotFound,
                            ErrorCode = (int)ErrorCodes.RoleNotFound
                        };
                    }
                    
                    UserRole userRole = new UserRole()
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    };
                    await _unitOfWork.UserRoles.CreateAsync(userRole);
                    await _unitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();

                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                }
            }
            
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
        var user = await _userRepository 
                .GetAll()
                .Include(x=>x.Roles)
                .FirstOrDefaultAsync(u => u.Login == dto.Login);
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

            var userRoles = user.Roles;
            var claims = userRoles
                .Select(x => new Claim(ClaimTypes.Role, x.Name))
                .ToList();
            claims
                .Add(new Claim(ClaimTypes.Name, user.Login));
            
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

                _userTokenRepository.Update(userToken);
                await _userTokenRepository.SaveChangesAsync();
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