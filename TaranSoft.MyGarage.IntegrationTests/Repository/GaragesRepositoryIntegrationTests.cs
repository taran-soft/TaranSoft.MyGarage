using Microsoft.EntityFrameworkCore;
using TaranSoft.MyGarage.Data.Models.EF;
using TaranSoft.MyGarage.Repository.EntityFramework;
using Xunit;

namespace TaranSoft.MyGarage.IntegrationTests.Repository;

public class GaragesRepositoryIntegrationTests : BaseIntegrationTest
{
    private readonly GaragesRepository _repository;

    public GaragesRepositoryIntegrationTests()
    {
        _repository = new GaragesRepository(DbContext);
    }

    private async Task<User> SetupCustomUserData(string email, string name, string nickname)
    {
        var user = new User
        {
            Name = name,
            Nickname = nickname,
            Email = email
        };
        DbContext.Users.Add(user);
        await DbContext.SaveChangesAsync();

        return user;
    }
    private async Task<User> SetupTestData()
    {
        var user = new User
        {
            Name = "testName",
            Nickname = "testNickName",
            Email = "test.email@email.com"
        };
        DbContext.Users.Add(user);
        await DbContext.SaveChangesAsync();

        return user;
    }

    [Fact]
    public async Task CreateAsync_ValidGarage_ReturnsCreatedGarage()
    {
        // Arrange
        var user = await SetupTestData();
        var garage = new UserGarage
        {
            OwnerId = user.Id
        };

        // Act
        await _repository.CreateAsync(garage);

        // Assert
        var createdGarage = await DbContext.Garages.FindAsync(garage.Id);
        Assert.NotNull(createdGarage);
        Assert.Equal(user.Id, createdGarage.OwnerId);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingGarage_ReturnsGarage()
    {
        // Arrange
        var user = await SetupTestData();
        var garage = new UserGarage
        {
            OwnerId = user.Id
        };
        DbContext.Garages.Add(garage);
        await DbContext.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(garage.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(garage.Id, result.Id);
        Assert.Equal(user.Id, result.OwnerId);
    }

    [Fact]
    public async Task UpdateAsync_ExistingGarage_ReturnsTrue()
    {
        // Arrange
        var user = await SetupTestData();
        var garage = new UserGarage
        {
            OwnerId = user.Id
        };
        DbContext.Garages.Add(garage);
        await DbContext.SaveChangesAsync();

        // Act
        var result = await _repository.UpdateAsync(garage);

        // Assert
        Assert.True(result);
        var updatedGarage = await DbContext.Garages.FindAsync(garage.Id);
        Assert.NotNull(updatedGarage);
    }

    [Fact]
    public async Task DeleteAsync_ExistingGarage_ReturnsTrue()
    {
        // Arrange
        var user = await SetupTestData();
        var garage = new UserGarage
        {
            OwnerId = user.Id
        };
        DbContext.Garages.Add(garage);
        await DbContext.SaveChangesAsync();

        // Act
        var result = await _repository.DeleteAsync(garage.Id);

        // Assert
        Assert.True(result);
        var deletedGarage = await DbContext.Garages.FindAsync(garage.Id);
        Assert.Null(deletedGarage);
    }

    [Fact]
    public async Task ListAllAsync_ReturnsAllGarages()
    {
        // Arrange
        var user1 = await SetupCustomUserData("user1@email.com", "testName1", "testNickName1");
        var user2 = await SetupCustomUserData("user2@email.com", "testName2", "testNickName2");
        
        var garages = new List<UserGarage>
        {
            new() { OwnerId = user1.Id },
            new() { OwnerId = user2.Id }
        };
        DbContext.Garages.AddRange(garages);
        await DbContext.SaveChangesAsync();

        // Act
        var result = await _repository.ListAllAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, g => g.OwnerId == user1.Id);
        Assert.Contains(result, g => g.OwnerId == user2.Id);
    }

    [Fact]
    public async Task SearchAsync_WithPagination_ReturnsCorrectGarages()
    {
        // Arrange
        var user1 = await SetupCustomUserData("user1@email.com", "testName1", "testNickName1");
        var user2 = await SetupCustomUserData("user2@email.com", "testName2", "testNickName2");
        var user3 = await SetupCustomUserData("user3@email.com", "testName3", "testNickName3");

        var garages = new List<UserGarage>
        {
            new() { OwnerId = user1.Id },
            new() { OwnerId = user2.Id },
            new() { OwnerId = user3.Id }
        };
        DbContext.Garages.AddRange(garages);
        await DbContext.SaveChangesAsync();

        // Act
        var result = await _repository.SearchAsync(take: 2, skip: 1);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, g => g.OwnerId == user2.Id);
        Assert.Contains(result, g => g.OwnerId == user3.Id);
    }

    [Fact]
    public async Task GetTotalCountAsync_ReturnsCorrectCount()
    {
        // Arrange
        // Verify database is clean
        var initialCount = await _repository.GetTotalCountAsync();
        Assert.Equal(0, initialCount);
        
        var user1 = await SetupCustomUserData("user1@email.com", "testName1", "testNickName1");
        var user2 = await SetupCustomUserData("user2@email.com", "testName2", "testNickName2");
        
        var garages = new List<UserGarage>
        {
            new() { OwnerId = user1.Id },
            new() { OwnerId = user2.Id }
        };
        DbContext.Garages.AddRange(garages);
        await DbContext.SaveChangesAsync();

        // Verify data was added
        var actualCount = await DbContext.Garages.CountAsync();
        Assert.Equal(2, actualCount);

        // Act
        var result = await _repository.GetTotalCountAsync();

        // Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public async Task GetByOwnerIdAsync_ExistingGarage_ReturnsGarage()
    {
        // Arrange
        var user = await SetupTestData();
        var garage = new UserGarage
        {
            OwnerId = user.Id
        };
        DbContext.Garages.Add(garage);
        await DbContext.SaveChangesAsync();

        // Act
        var result = await _repository.GetByOwnerIdAsync(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(garage.Id, result.Id);
        Assert.Equal(user.Id, result.OwnerId);
    }

    [Fact]
    public async Task GetByOwnerIdAsync_NonExistingGarage_ReturnsNull()
    {
        // Arrange
        var user = await SetupTestData();

        // Act
        var result = await _repository.GetByOwnerIdAsync(user.Id);

        // Assert
        Assert.Null(result);
    }
} 