using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Rhino.Mocks;
using MyGarage.Tests;
using TaranSoft.MyGarage.Services.Interfaces;
using TaranSoft.MyGarage.Repository.Interfaces;
using TaranSoft.MyGarage.Contracts;
using TaranSoft.MyGarage.Services;
using TaranSoft.MyGarage.Contracts.Request;
using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TaranSoft.MyGarage.Data.Models.MongoDB;

namespace TaranSoft.MyGarage.Tests.Service;

[TestFixture]
public class UsersServiceTest
{
    private Mock<IUserRepository> _userRepositoryMock;
    private IUsersService _usersService;
    private Mock<IPasswordHasher<Services.Models.User>> _passwordHasherMock;
    private Mock<IIdGenerator> _idGeneratorMock;
    private Mock<IOptions<AppSettings>> _appSettings;
    private Mock<ILogger<UsersService>> _usersServiceLoggerMock;
    private Mock<IMapper> _mapperMock;

    [SetUp]
    public void Setup()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _appSettings = new Mock<IOptions<AppSettings>>();
        _passwordHasherMock = new Mock<IPasswordHasher<Services.Models.User>>();
        _idGeneratorMock = new Mock<IIdGenerator>();
        _usersServiceLoggerMock = new Mock<ILogger<UsersService>>();
        _mapperMock = new Mock<IMapper>();

        _usersService = new UsersService(_userRepositoryMock.Object, _appSettings.Object, _passwordHasherMock.Object, _idGeneratorMock.Object, _usersServiceLoggerMock.Object, _mapperMock.Object);

    }

    [Test]
    public async Task TryToRegisterUser_Success()
    {
        //Given 
        
        var userRequest = new UserDto
        {
            Email = "mygarage@g.c",
            Nickname = "My Garage Username",
            Password = "mygaragepassword1!"
        };

        var expectedId = 1.ToGuid();
        _idGeneratorMock.Setup(x => x.NewGuid()).Returns(expectedId);
        _userRepositoryMock
            .Setup(x => x.Create(It.IsAny<User>()))
            .Returns(Task.FromResult(expectedId));

        _passwordHasherMock.Setup(x => x.HashPassword(Arg<Services.Models.User>.Is.Anything, Arg<string>.Is.Anything)).Returns(string.Empty);
        
        // When
        
        var id = await _usersService.Register(userRequest);
        
        //Then
        
        Assert.AreEqual(expectedId, id);
        
    }
    
    [Test]
    public async Task TryToRegisterUser_Fail_UserExists()
    {
        //Given 
        var userRequest = new UserDto
        {
            Email = "mygarage@g.c",
            Nickname = "My Garage Username",
            Password = "mygaragepassword1!"
        };

        var expectedId = 1.ToGuid();
        _idGeneratorMock.Setup(x => x.NewGuid()).Returns(expectedId);
        
        // When
        
        _userRepositoryMock
            .Setup(x => x.GetByEmail(It.IsAny<string>()))
            .Returns(Task.FromResult(new User()));
        
        //Then
        
        var ex = Assert.ThrowsAsync<ApplicationException>(() => _usersService.Register(userRequest));
        Assert.IsNotNull(ex);
        Assert.That(ex.Message, Is.EqualTo("User already exists"));
        
    }
}