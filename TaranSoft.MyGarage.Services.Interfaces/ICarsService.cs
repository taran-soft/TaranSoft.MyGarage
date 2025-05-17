using TaranSoft.MyGarage.Contracts.Dto.Vehicle;
namespace TaranSoft.MyGarage.Services.Interfaces;

public interface ICarsService
{
    public Task<IList<CarDto>> Search(int take, int skip);
    Task Update(Guid id, CarDto carDto);
    Task<Guid> Create(CarDto car);
    Task Delete(Guid id);
    Task<IList<CarDto>> GetByUserId(Guid id);
}