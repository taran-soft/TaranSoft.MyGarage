using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Xunit;
using MyGarage.Tests;
using TaranSoft.MyGarage.Services.Interfaces;
using TaranSoft.MyGarage.Repository.Interfaces;
using TaranSoft.MyGarage.Contracts;
using TaranSoft.MyGarage.Services;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TaranSoft.MyGarage.Data.Models.MongoDB;
using TaranSoft.MyGarage.Contracts.Dto;
using NSubstitute;

namespace TaranSoft.MyGarage.UnitTests.Service;

public class UsersServiceTest
{
    private IUserRepository _userRepositoryMock;
    private IUsersService _usersService;
    private IPasswordHasher<Services.Models.User> _passwordHasherMock;
    private IOptions<AppSettings> _appSettings;
    private ILogger<UsersService> _usersServiceLoggerMock;
    private IMapper _mapperMock;

    public UsersServiceTest()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _appSettings = Substitute.For<IOptions<AppSettings>>();
        _passwordHasherMock = Substitute.For<IPasswordHasher<Services.Models.User>>();
        _usersServiceLoggerMock = Substitute.For<ILogger<UsersService>>();
        _mapperMock = Substitute.For<IMapper>();

        _usersService = new UsersService(_userRepositoryMock, _appSettings, _passwordHasherMock, _usersServiceLoggerMock, _mapperMock);
    }

    [Fact]
    public async Task TryToRegisterUser_Success()
    {
        //Given 
        
        var userRequest = new UserDto
        {
            Email = "mygarage@g.c",
            Nickname = "My Garage Username",
            Password = "mygaragepassword1!"
        };

        var expectedId = 1L;
        _userRepositoryMock.Create(Arg.Any<User>()).Returns(Task.FromResult(expectedId));

        _passwordHasherMock.HashPassword(Arg.Any<Services.Models.User>(), Arg.Any<string>()).Returns(string.Empty);
        
        // When
        
        var id = await _usersService.Register(userRequest);
        
        //Then
        
        Assert.Equal(expectedId, id);
        
    }
    
    [Fact]
    public async Task TryToRegisterUser_Fail_UserExists()
    {
        //Given 
        var userRequest = new UserDto
        {
            Email = "mygarage@g.c",
            Nickname = "My Garage Username",
            Password = "mygaragepassword1!"
        };

        var expectedId = 1L;
        
        // When
        
        _userRepositoryMock.GetByEmail(Arg.Any<string>()).Returns(Task.FromResult(new User()));
        
        //Then
        
        var ex = await Assert.ThrowsAsync<ApplicationException>(() => _usersService.Register(userRequest));
        Assert.NotNull(ex);
        Assert.Equal("User already exists", ex?.Message);
        
    }
}