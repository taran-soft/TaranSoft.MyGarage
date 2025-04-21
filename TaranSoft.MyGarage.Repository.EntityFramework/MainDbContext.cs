using Microsoft.EntityFrameworkCore;
using TaranSoft.MyGarage.Data.Models;
using TaranSoft.MyGarage.Data.Models.EF;

namespace TaranSoft.MyGarage.Repository.EntityFramework
{
    public class MainDbContext : DbContext
    {
        //public DbSet<Data.Models.EF.User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        //public DbSet<Manufacturer> Manufacturers { get; set; }
        //public DbSet<Country> Countries { get; set; }
        //public DbSet<UserGarage> UserGarage { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) 
        {
        }
    }
}
