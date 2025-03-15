
using TaranSoft.MyGarage.Data.Models;

namespace TaranSoft.MyGarage.Repository.Interfaces;

public interface ICarsRepository
{
    Task<IList<Car>> Search(int take, int skip);
    Task<IList<Car>> ListAll();
    Task<Car> GetById(Guid id);
    Task<IList<Car>> GetByUserId(Guid id);
    Task<Guid> Create(Car car);
    Task Update(Guid id, Car user);
    Task Delete(Guid id);
}