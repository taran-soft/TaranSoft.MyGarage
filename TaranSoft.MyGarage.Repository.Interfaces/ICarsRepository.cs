using TaranSoft.MyGarage.Data.Models.MongoDB;

namespace TaranSoft.MyGarage.Repository.Interfaces;

public interface ICarsRepository
{
    Task<IList<Car>> Search(int take, int skip);
    Task<IList<Car>> ListAll();
    Task<Car> GetById(long id);
    Task<IList<Car>> GetByUserId(long id);
    Task<long> Create(Car car);
    Task Update(long id, Car user);
    Task Delete(long id);
}