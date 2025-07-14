using Microsoft.EntityFrameworkCore;
using TaranSoft.MyGarage.Data.Models.EF;
using TaranSoft.MyGarage.Data.Models.EF.CarJournal;
using TaranSoft.MyGarage.Data.Models.EF.Vehicles;
using TaranSoft.MyGarage.Repository.EntityFramework;
using Xunit;

namespace TaranSoft.MyGarage.IntegrationTests.Repository;

public class JournalsRepositoryIntegrationTests : BaseIntegrationTest
{
    private readonly JournalsRepository _repository;

    public JournalsRepositoryIntegrationTests()
    {
        _repository = new JournalsRepository(DbContext);
    }

    private async Task<(User user, Manufacturer manufacturer, UserGarage garage, Car car)> SetupTestData()
    {
        var user = new User
        {
            Name = "testName",
            Nickname = "testNickName",
            Email = "test.email@email.com"
        };
        DbContext.Users.Add(user);
        await DbContext.SaveChangesAsync();

        var manufacturer = new Manufacturer
        {
            ManufacturerName = ManufacturerEnum.Toyota,
            YearCreation = 1937
        };
        DbContext.Manufacturers.Add(manufacturer);
        await DbContext.SaveChangesAsync();

        var garage = new UserGarage
        {
            OwnerId = user.Id
        };
        DbContext.Garages.Add(garage);
        await DbContext.SaveChangesAsync();

        var car = new Car
        {
            Name = "Test Car",
            ManufacturerId = manufacturer.Id,
            GarageId = garage.Id,
            LicensePlate = "ABC123"
        };
        DbContext.Cars.Add(car);
        await DbContext.SaveChangesAsync();

        return (user, manufacturer, garage, car);
    }

    private async Task<(User user1, User user2, Manufacturer manufacturer, UserGarage garage1, UserGarage garage2, Car car1, Car car2)> SetupMultipleTestData()
    {
        var user1 = new User
        {
            Name = "testName1",
            Nickname = "testNickName1",
            Email = "test1.email@email.com"
        };
        var user2 = new User
        {
            Name = "testName2",
            Nickname = "testNickName2",
            Email = "test2.email@email.com"
        };
        DbContext.Users.AddRange(user1, user2);
        await DbContext.SaveChangesAsync();

        var manufacturer = new Manufacturer
        {
            ManufacturerName = ManufacturerEnum.Toyota,
            YearCreation = 1937
        };
        DbContext.Manufacturers.Add(manufacturer);
        await DbContext.SaveChangesAsync();

        var garage1 = new UserGarage
        {
            OwnerId = user1.Id
        };
        var garage2 = new UserGarage
        {
            OwnerId = user2.Id
        };
        DbContext.Garages.AddRange(garage1, garage2);
        await DbContext.SaveChangesAsync();

        var car1 = new Car
        {
            Name = "Test Car 1",
            ManufacturerId = manufacturer.Id,
            GarageId = garage1.Id,
            LicensePlate = "ABC123"
        };
        var car2 = new Car
        {
            Name = "Test Car 2",
            ManufacturerId = manufacturer.Id,
            GarageId = garage2.Id,
            LicensePlate = "DEF456"
        };
        DbContext.Cars.AddRange(car1, car2);
        await DbContext.SaveChangesAsync();

        return (user1, user2, manufacturer, garage1, garage2, car1, car2);
    }

    [Fact]
    public async Task AddAsync_ValidJournal_AddsJournalToDatabase()
    {
        // Arrange
        var (user, manufacturer, garage, car) = await SetupTestData();
        var journal = new Journal
        {
            VehicleId = car.Id,
            CreatedById = user.Id,
            Title = "Test Journal"
        };

        // Act
        await _repository.AddAsync(journal);

        // Assert
        var addedJournal = await DbContext.Journals.FindAsync(journal.Id);
        Assert.NotNull(addedJournal);
        Assert.Equal(journal.Title, addedJournal.Title);
        Assert.Equal(car.Id, addedJournal.VehicleId);
        Assert.Equal(user.Id, addedJournal.CreatedById);
    }

    [Fact]
    public async Task GetByVehicleId_ExistingJournal_ReturnsJournal()
    {
        // Arrange
        var (user, manufacturer, garage, car) = await SetupTestData();
        var journal = new Journal
        {
            VehicleId = car.Id,
            CreatedById = user.Id,
            Title = "Test Journal"
        };
        DbContext.Journals.Add(journal);
        await DbContext.SaveChangesAsync();

        // Act
        var result = await _repository.GetByVehicleId(car.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(journal.Title, result.First().Title);
        Assert.Equal(car.Id, result.First().VehicleId);
    }

    [Fact]
    public async Task UpdateAsync_ExistingJournal_UpdatesJournal()
    {
        // Arrange
        var (user, manufacturer, garage, car) = await SetupTestData();
        var journal = new Journal
        {
            VehicleId = car.Id,
            CreatedById = user.Id,
            Title = "Test Journal"
        };
        DbContext.Journals.Add(journal);
        await DbContext.SaveChangesAsync();

        // Act
        journal.Title = "Updated Journal";
        await _repository.UpdateAsync(journal);

        // Assert
        var updatedJournal = await DbContext.Journals.FindAsync(journal.Id);
        Assert.Equal("Updated Journal", updatedJournal.Title);
    }

    [Fact]
    public async Task DeleteAsync_ExistingJournal_RemovesJournal()
    {
        // Arrange
        var (user, manufacturer, garage, car) = await SetupTestData();
        var journal = new Journal
        {
            VehicleId = car.Id,
            CreatedById = user.Id,
            Title = "Test Journal"
        };
        DbContext.Journals.Add(journal);
        await DbContext.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(journal.Id);

        // Assert
        var deletedJournal = await DbContext.Journals.FindAsync(journal.Id);
        Assert.Null(deletedJournal);
    }

    [Fact]
    public async Task SearchAsync_WithPagination_ReturnsCorrectJournals()
    {
        // Arrange
        var (user1, user2, manufacturer, garage1, garage2, car1, car2) = await SetupMultipleTestData();
        var journals = new List<Journal>
        {
            new() { VehicleId = car1.Id, CreatedById = user1.Id, Title = "Journal Car 1" },
            new() { VehicleId = car2.Id, CreatedById = user2.Id, Title = "Journal Car 2" },
        };
        DbContext.Journals.AddRange(journals);
        await DbContext.SaveChangesAsync();

        // Act
        var result = await _repository.SearchAsync(take: 2, skip: 0);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, j => j.Title == "Journal Car 1");
        Assert.Contains(result, j => j.Title == "Journal Car 2");
    }

    [Fact]
    public async Task GetTotalCountAsync_ReturnsCorrectCount()
    {
        // Arrange
        var (user1, user2, manufacturer, garage1, garage2, car1, car2) = await SetupMultipleTestData();
        var journals = new List<Journal>
        {
            new() { VehicleId = car1.Id, CreatedById = user1.Id, Title = "Journal 1" },
            new() { VehicleId = car2.Id, CreatedById = user2.Id, Title = "Journal 2" }
        };
        DbContext.Journals.AddRange(journals);
        await DbContext.SaveChangesAsync();

        // Act
        var result = await _repository.GetTotalCountAsync();

        // Assert
        Assert.Equal(2, result);
    }
} 