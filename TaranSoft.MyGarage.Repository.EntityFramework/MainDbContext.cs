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
        public DbSet<Country> Countries { get; set; }
        //public DbSet<UserGarage> UserGarage { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>()
                .Property(m => m.ManufacturerName)
                .HasConversion<string>();

            modelBuilder.Entity<Manufacturer>()
                .HasOne<Country>();

            modelBuilder.Entity<Data.Models.EF.Car>()
                .HasOne(c => c.Manufacturer);
                //.WithMany(m => m.Cars)
                //.HasForeignKey(c => c.ManufacturerId);
            
            base.OnModelCreating(modelBuilder);

        }
    }
}
