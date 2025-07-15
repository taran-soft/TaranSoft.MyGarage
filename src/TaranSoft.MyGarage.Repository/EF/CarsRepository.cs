using Microsoft.EntityFrameworkCore;
using TaranSoft.MyGarage.Data.Models.EF.Vehicles;
using TaranSoft.MyGarage.Repository.Interfaces.EF;

namespace TaranSoft.MyGarage.Repository.EF;

/// <summary>
/// Implementation of IEFCarsRepository using Entity Framework Core.
/// </summary>
public class CarsRepository : IEFCarsRepository
{
    private readonly DbContext _context;
    private readonly DbSet<Car> _cars;

    public CarsRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cars = context.Set<Car>();
    }

    /// <inheritdoc />
    public async Task<IList<Car>> SearchAsync(int take, int skip)
    {
        return await _cars
            .OrderBy(c => c.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<int> GetTotalCountAsync()
    {
        return await _cars.CountAsync();
    }

    /// <inheritdoc />
    public async Task<IList<Car>> ListAllAsync()
    {
        return await _cars.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Car?> GetByIdAsync(Guid id)
    {
        return await _cars.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<Car> CreateAsync(Car car)
    {
        if (car == null)
            throw new ArgumentNullException(nameof(car));

        var entry = await _cars.AddAsync(car);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Car car)
    {
        if (car == null)
            throw new ArgumentNullException(nameof(car));

        var existingCar = await _cars.FindAsync(car.Id);
        if (existingCar == null)
            return false;

        _context.Entry(existingCar).CurrentValues.SetValues(car);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Guid id)
    {
        var car = await _cars.FindAsync(id);
        if (car == null)
            return false;

        _cars.Remove(car);
        await _context.SaveChangesAsync();
        return true;
    }
} 