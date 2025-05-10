using Microsoft.EntityFrameworkCore;
using TaranSoft.MyGarage.Data.Models.EF;
using TaranSoft.MyGarage.Data.Models.EF.Vehicles;

namespace TaranSoft.MyGarage.Repository.EntityFramework
{
    public class MainDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<UserGarage> Garages { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>()
                .HasOne(c => c.Garage)
                .WithMany(common => common.Vehicles)
                .HasForeignKey(c => c.GarageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(b => b.Manufacturer);

            modelBuilder.Entity<Motorcycle>()
                .HasOne(b => b.Manufacturer);

            modelBuilder.Entity<Trailer>()
                .HasOne(b => b.Manufacturer);

            modelBuilder.Entity<Manufacturer>()
                .HasOne(c => c.ManufacturerCountry);

            modelBuilder.Entity<Manufacturer>()
                .Property(m => m.ManufacturerName)
                .HasConversion<string>();

            modelBuilder.Entity<UserGarage>()
                .HasOne(g => g.Owner);

        }
    }
}
