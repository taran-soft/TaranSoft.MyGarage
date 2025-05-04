using TaranSoft.MyGarage.Data.Models.EF;

namespace TaranSoft.MyGarage.Repository.Interfaces;

public interface IEFGaragesRepository
{
    Task<IList<UserGarage>> Search(int take, int skip);
    Task<List<UserGarage>> ListAllAsync();
    //Task<Car?> GetByIdAsync(Guid id);
    //Task<IList<Car>> GetByUserId(Guid id);
    //Task<Car> CreateAsync(Car car);
    //Task<bool> UpdateAsync(Car car);
    //Task<bool> DeleteAsync(Guid id);
}