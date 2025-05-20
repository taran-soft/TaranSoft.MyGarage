using TaranSoft.MyGarage.Data.Models.EF;

namespace TaranSoft.MyGarage.Repository.Interfaces.EF;

public interface IEFGaragesRepository
{
    Task<IList<UserGarage>> Search(int take, int skip);
    Task<List<UserGarage>> ListAllAsync();
    Task<UserGarage?> GetByUserId(long id);
    
    //Task<Car> CreateAsync(Car car);
    //Task<bool> UpdateAsync(Car car);
    //Task<bool> DeleteAsync(Guid id);
}