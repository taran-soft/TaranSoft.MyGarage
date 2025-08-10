using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GarageSpace.Repository.EntityFramework;

namespace GarageSpace.IntegrationTests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        public async Task InitializeAsync()
        {
            await CleanupDatabase();
        }

        public async Task DisposeAsync()
        {
            // Optionally clean up after all tests
            await CleanupDatabase();
        }

        public async Task CleanupDatabase()
        {
            // Load configuration (adjust path as needed)
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .Options;

            using var dbContext = new MainDbContext(options);

            // Remove all data from all tables
            dbContext.Countries.RemoveRange(dbContext.Countries);
            dbContext.Journals.RemoveRange(dbContext.Journals);
            dbContext.JournalRecords.RemoveRange(dbContext.JournalRecords);
            dbContext.Cars.RemoveRange(dbContext.Cars);
            dbContext.Garages.RemoveRange(dbContext.Garages);
            dbContext.Users.RemoveRange(dbContext.Users);
            dbContext.Manufacturers.RemoveRange(dbContext.Manufacturers);

            await dbContext.SaveChangesAsync();
        }
    }
}
