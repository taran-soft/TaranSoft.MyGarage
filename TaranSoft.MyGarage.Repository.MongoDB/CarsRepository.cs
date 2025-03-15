using MongoDB.Bson;
using TaranSoft.MyGarage.Repository.Interfaces;
using TaranSoft.MyGarage.Data.Models;
using TaranSoft.MyGarage.Repository.MongoDB.DbContext;
using MongoDB.Driver;

namespace TaranSoft.MyGarage.Repository.MongoDB;

public class CarsRepository : ICarsRepository
{
    private readonly IMongoDbContext _dbContext;
    
    public CarsRepository(IMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Car>> Search(int take, int skip)
    {
        var result =  await _dbContext.Cars.Find(new BsonDocument()).ToListAsync();
        
        return result
            .Take(take)
            .Skip(skip)
            .ToList();
    }

    public async Task<IList<Car>> ListAll()
    {
        return await _dbContext.Cars.Find(c => true).ToListAsync();
    }

    public async Task<Car> GetById(Guid id)
    {
        return await _dbContext.Cars.Find(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IList<Car>> GetByUserId(Guid userId)
    {
        return await _dbContext.Cars.Find(c => c.CreatedBy == userId).ToListAsync();
    }

    public async Task<Guid> Create(Car car)
    {
        await _dbContext.Cars.InsertOneAsync(car);
        return car.Id;
    }

    public async Task Update(Guid id, Car car)
    {
        await _dbContext.Cars.ReplaceOneAsync(c => c.Id == id, car);
    }

    public async Task Delete(Guid id)
    {
        await _dbContext.Cars.DeleteOneAsync(c => c.Id == id);
    }
}