using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaranSoft.MyGarage.Services.Interfaces;
using TaranSoft.MyGarage.Repository.Interfaces;
using TaranSoft.MyGarage.Contracts;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TaranSoft.MyGarage.Data.Models.MongoDB;
using TaranSoft.MyGarage.Contracts.Dto;

namespace TaranSoft.MyGarage.Services;

public class UsersService : IUsersService
{
    private readonly IUserRepository _userRepository;
    private readonly AppSettings _appSettings;
    private readonly IPasswordHasher<Models.User> _passwordHashService;
    private readonly ILogger<UsersService> _logger;
    private readonly IMapper _mapper;

    public UsersService(
        IUserRepository userRepository,
        IOptions<AppSettings> appSettings,
        IPasswordHasher<Models.User> passwordHashService,
        ILogger<UsersService> logger,
        IMapper mapper
        )
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
        _appSettings = appSettings.Value;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<string?> GetToken(string email, string password)
    {
        var userEntity = await _userRepository.GetByEmail(email);

        var user = new Models.User
        {
            Id = userEntity.Id,
            Email = userEntity.Email,
            Nickname = userEntity.Nickname,
            Password = userEntity.Password
        };

        var passwordVerificationResult = _passwordHashService.VerifyHashedPassword(user, user.Password, password);
        if (passwordVerificationResult != PasswordVerificationResult.Success)
        {
            throw new ApplicationException("Password Hash Verification failed");
        }

        var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecretKey);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.UserData, user.Id.ToString()),
                }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<long> Register(UserDto userRequest)
    {
        var userExistsEmailCheck = await CheckUserExists("email", userRequest.Email);
        if (userExistsEmailCheck)
        {
            throw new ApplicationException("User already exists");
        }
        
        var userExistsNicknameCheck = await CheckUserExists("nickname", userRequest.Nickname);
        if (userExistsNicknameCheck)
        {
            throw new ApplicationException("User already exists");
        }

        var user = new Models.User
        {
            Email = userRequest.Email,
            Nickname = userRequest.Nickname,
        };
        
        var hashedPassword = _passwordHashService.HashPassword(user, userRequest.Password);
        user.Password = hashedPassword;
        
        var hashedUser = _mapper.Map<User>(user);
        try
        {
            return await _userRepository.Create(hashedUser);
        }
        catch (Exception ex)
        {
            _logger.LogError("User '{0}' creation failed with error {1}", user.Email, ex.Message);
            throw;
        }
    }

    public async Task<bool> CheckUserExists(string property, string value)
    {
        switch (property)
        {
            case "email":
            {
                var user = await _userRepository.GetByEmail(value);
                return user != null;
            }
            case "nickname":
            {
                var user = await _userRepository.GetByNickname(value);
                return user != null;
            }
            default:
                return false;
        }
    }

    public async Task<UserDto> GetUserById(long id)
    {
        var user = await _userRepository.GetById(id);
        return _mapper.Map<UserDto>(user);
    }

    public Task<UserDto> GetByNickname(string name)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUser(long id, UserDto user)
    {
        throw new NotImplementedException();
    }
}