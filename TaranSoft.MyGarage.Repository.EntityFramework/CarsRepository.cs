using Microsoft.EntityFrameworkCore;
using TaranSoft.MyGarage.Repository.Interfaces;
using Car = TaranSoft.MyGarage.Data.Models.EF.Car;

namespace TaranSoft.MyGarage.Repository.EntityFramework;

public class CarsRepository : BaseRepository<Car>, IEFCarsRepository
{
    private readonly MainDbContext _context;

    public CarsRepository(MainDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Car> CreateAsync(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return car;
    }

    // ✅ Read (Get all cars)
    public async Task<List<Car>> ListAllAsync()
    {
        return await _context.Cars
            .Include(c => c.Manufacturer)
            .ToListAsync();
    }

    // ✅ Read (Get single car by ID)
    public async Task<Car?> GetByIdAsync(Guid id)
    {
        return await _context.Cars
            .Include(c => c.Manufacturer)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    // ✅ Update
    public async Task<bool> UpdateAsync(Car updatedCar)
    {
        var existingCar = await _context.Cars.FindAsync(updatedCar.Id);

        if (existingCar == null)
            return false;

        existingCar.Name = updatedCar.Name;
        existingCar.ManufacturerId = updatedCar.ManufacturerId;

        await _context.SaveChangesAsync();
        return true;
    }

    // ✅ Delete
    public async Task<bool> DeleteAsync(Guid id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null)
            return false;

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IList<Car>> Search(int take, int skip)
    {
        return await _context.Cars
        .Include(c => c.Manufacturer)
        .OrderBy(c => c.Name) // Optional: Sort by Name
        .Skip(skip)
        .Take(take)
        .ToListAsync();
    }
   
}