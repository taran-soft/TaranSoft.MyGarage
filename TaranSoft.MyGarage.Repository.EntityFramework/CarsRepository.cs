using TaranSoft.MyGarage.Repository.Interfaces;
using TaranSoft.MyGarage.Data.Models;

namespace TaranSoft.MyGarage.Repository.EntityFramework;

public class CarsRepository : BaseRepository<Car>, ICarsRepository
{
    public CarsRepository(MainDbContext dbContext): base(dbContext)
    {
    }

    public async Task<IList<Car>> Search(int take, int skip)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Car>> ListAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Car> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Car>> GetByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> Create(Car car)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Guid id, Car car)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}