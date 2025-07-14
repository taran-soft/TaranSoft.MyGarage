using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaranSoft.MyGarage.Repository.EntityFramework;

namespace TaranSoft.MyGarage.IntegrationTests;

public abstract class BaseIntegrationTest : IDisposable
{
    protected readonly MainDbContext DbContext;
    protected readonly IConfiguration Configuration;

    protected BaseIntegrationTest()
    {
        // Load configuration from appsettings.json
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        // Setup in-memory database for testing with unique name
        var databaseName = $"TestDb_{Guid.NewGuid()}";
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        DbContext = new MainDbContext(options);
        
        // Ensure database is created
        DbContext.Database.EnsureCreated();
    }

    protected async Task CleanupDatabase()
    {
        // Remove all data from all tables
        DbContext.Journals.RemoveRange(DbContext.Journals);
        DbContext.JournalRecords.RemoveRange(DbContext.JournalRecords);
        DbContext.Cars.RemoveRange(DbContext.Cars);
        DbContext.Garages.RemoveRange(DbContext.Garages);
        DbContext.Users.RemoveRange(DbContext.Users);
        DbContext.Manufacturers.RemoveRange(DbContext.Manufacturers);
        
        await DbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
} 