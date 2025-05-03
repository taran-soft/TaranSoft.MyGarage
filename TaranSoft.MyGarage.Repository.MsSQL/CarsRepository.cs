using Microsoft.EntityFrameworkCore;
using TaranSoft.MyGarage.Data.Models.EF;
using TaranSoft.MyGarage.Repository.EntityFramework;

public class CarsRepository
{
    private readonly MainDbContext _context;

    public CarsRepository(MainDbContext context)
    {
        _context = context;
    }

    // ✅ Create
    public async Task<Car> AddCarAsync(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return car;
    }

    // ✅ Read (Get all cars)
    public async Task<List<Car>> GetAllCarsAsync()
    {
        return await _context.Cars
            .Include(c => c.Manufacturer)
            .ToListAsync();
    }

    // ✅ Read (Get single car by ID)
    public async Task<Car?> GetCarByIdAsync(Guid id)
    {
        return await _context.Cars
            .Include(c => c.Manufacturer)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    // ✅ Update
    public async Task<bool> UpdateCarAsync(Car updatedCar)
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
    public async Task<bool> DeleteCarAsync(Guid id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null)
            return false;

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        return true;
    }
}
