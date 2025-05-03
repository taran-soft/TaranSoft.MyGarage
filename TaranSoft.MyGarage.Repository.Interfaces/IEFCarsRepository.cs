using Car = TaranSoft.MyGarage.Data.Models.EF.Car;

namespace TaranSoft.MyGarage.Repository.Interfaces;

public interface IEFCarsRepository
{
    Task<IList<Car>> Search(int take, int skip);
    Task<List<Car>> ListAllAsync();
    Task<Car?> GetByIdAsync(Guid id);
    //Task<IList<Car>> GetByUserId(Guid id);
    Task<Car> CreateAsync(Car car);
    Task<bool> UpdateAsync(Car car);
    Task<bool> DeleteAsync(Guid id);
}