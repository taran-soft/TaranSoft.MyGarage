﻿using TaranSoft.MyGarage.Contracts.Dto;

namespace TaranSoft.MyGarage.Services.Interfaces;

public interface IUsersService
{
    public Task<string?> GetToken(string email, string password);
    public Task<long> Register(UserDto user);
    public Task<bool> CheckUserExists(string property, string value);
    public Task<UserDto> GetUserById(long id);
    Task<UserDto> GetByNickname(string name);

    Task<bool> UpdateUser(long id, UserDto user);
}