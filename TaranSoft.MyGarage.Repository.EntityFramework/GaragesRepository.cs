using Microsoft.EntityFrameworkCore;
using TaranSoft.MyGarage.Repository.Interfaces.EF;
using UserGarage = TaranSoft.MyGarage.Data.Models.EF.UserGarage;

namespace TaranSoft.MyGarage.Repository.EntityFramework;

public class GaragesRepository : BaseRepository<UserGarage>, IEFGaragesRepository
{
    private readonly MainDbContext _context;

    public GaragesRepository(MainDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserGarage?> GetByUserId(long userId)
    {
        return await _context.Garages.FirstOrDefaultAsync(x => x.OwnerId == userId);
    }


    // ✅ Read (Get all cars)
    public async Task<List<UserGarage>> ListAllAsync()
    {
        return await _context.Garages.ToListAsync();
    }

    //// ✅ Read (Get single car by ID)
    //public async Task<UserGarage?> GetByIdAsync(long id)
    //{
    //    return await _context.Garages
    //        .Include(c => c.Manufacturer)
    //        .FirstOrDefaultAsync(c => c.Id == id);
    //}

    //// ✅ Update
    //public async Task<bool> UpdateAsync(UserGarage updatedCar)
    //{
    //    var existingCar = await _context.Cars.FindAsync(updatedCar.Id);

    //    if (existingCar == null)
    //        return false;

    //    existingCar.Name = updatedCar.Name;
    //    existingCar.ManufacturerId = updatedCar.ManufacturerId;

    //    await _context.SaveChangesAsync();
    //    return true;
    //}

    //// ✅ Delete
    //public async Task<bool> DeleteAsync(long id)
    //{
    //    var car = await _context.Cars.FindAsync(id);
    //    if (car == null)
    //        return false;

    //    _context.Cars.Remove(car);
    //    await _context.SaveChangesAsync();
    //    return true;
    //}

    public async Task<IList<UserGarage>> Search(int take, int skip)
    {
        return await _context.Garages
        .Skip(skip)
        .Take(take)
        .Include(g => g.Cars)
        .ThenInclude(c => c.Manufacturer)
        .ThenInclude(m => m.ManufacturerCountry)
        .Include(g => g.Motorcycles)
        .ThenInclude(m => m.Manufacturer)
        .ThenInclude(m => m.ManufacturerCountry)
        .Include(g => g.Trailers)
        .ThenInclude(m => m.Manufacturer)
        .ThenInclude(m => m.ManufacturerCountry)
        .Include(g => g.Owner)
        .ToListAsync();
    }
}