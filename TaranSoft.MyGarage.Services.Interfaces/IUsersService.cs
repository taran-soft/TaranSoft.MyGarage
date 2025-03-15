using TaranSoft.MyGarage.Contracts;

namespace TaranSoft.MyGarage.Services.Interfaces;

public interface IUsersService
{
    public Task<string?> GetToken(string email, string password);
    public Task<Guid> Register(UserDto user);
    public Task<bool> CheckUserExists(string property, string value);
    public Task<UserDto> GetUserById(Guid id);
    Task<UserDto> GetByNickname(string name);

    Task<bool> UpdateUser(Guid id, UserDto user);
}