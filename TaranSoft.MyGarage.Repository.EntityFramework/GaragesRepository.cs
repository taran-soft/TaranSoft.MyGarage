using Microsoft.EntityFrameworkCore;
using TaranSoft.MyGarage.Repository.Interfaces;
using UserGarage = TaranSoft.MyGarage.Data.Models.EF.UserGarage;

namespace TaranSoft.MyGarage.Repository.EntityFramework;

public class GaragesRepository : BaseRepository<UserGarage>, IEFGaragesRepository
{
    private readonly MainDbContext _context;

    public GaragesRepository(MainDbContext context) : base(context)
    {
        _context = context;
    }
        

    // ✅ Read (Get all cars)
    public async Task<List<UserGarage>> ListAllAsync()
    {
        throw new NotImplementedException();
        //return await _context.Cars
        //    .Include(c => c.Manufacturer)
        //    .ToListAsync();
    }

    // ✅ Read (Get single car by ID)
    public async Task<UserGarage?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
        //return await _context.Cars
        //    .Include(c => c.Manufacturer)
        //    .FirstOrDefaultAsync(c => c.Id == id);
    }

    // ✅ Update
    public async Task<bool> UpdateAsync(UserGarage updatedCar)
    {
        throw new NotImplementedException();
        //var existingCar = await _context.Cars.FindAsync(updatedCar.Id);

        //if (existingCar == null)
        //    return false;

        //existingCar.Name = updatedCar.Name;
        //existingCar.ManufacturerId = updatedCar.ManufacturerId;

        //await _context.SaveChangesAsync();
        //return true;
    }

    // ✅ Delete
    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
        //var car = await _context.Cars.FindAsync(id);
        //if (car == null)
        //    return false;

        //_context.Cars.Remove(car);
        //await _context.SaveChangesAsync();
        //return true;
    }

    public async Task<IList<Data.Models.EF.UserGarage>> Search(int take, int skip)
    {
        return await _context.Garages
        .Skip(skip)
        .Take(take)
        .Include(g => g.Cars)
        .ThenInclude(c => c.Manufacturer)
        .ThenInclude(m => m.ManufacturerCountry)
        .Include(g => g.Motocycles)
        .ThenInclude(m => m.Manufacturer)
        .ThenInclude(m => m.ManufacturerCountry)
        .Include(g => g.Owner)
        .ToListAsync();
    }
}