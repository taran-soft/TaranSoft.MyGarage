using Microsoft.EntityFrameworkCore;
using TaranSoft.MyGarage.Data.Models.EF;
using TaranSoft.MyGarage.Data.Models.MongoDB;

namespace TaranSoft.MyGarage.Repository.EntityFramework
{
    public class MainDbContext : DbContext
    {
        //public DbSet<Data.Models.EF.User> Users { get; set; }
        public DbSet<Data.Models.EF.Car> Cars { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        //public DbSet<Country> Countries { get; set; }
        //public DbSet<UserGarage> UserGarage { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) 
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer("Server=localhost,1433;Database=CarGarage;User Id=sa;Password=Passw0rd123;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Data.Models.EF.Car>()
            .HasOne(c => c.Manufacturer)
            .WithMany(m => m.Cars)
            .HasForeignKey(c => c.ManufacturerId);

        }
    }
}
