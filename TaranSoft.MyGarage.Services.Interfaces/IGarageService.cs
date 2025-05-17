using TaranSoft.MyGarage.Contracts.Dto;

namespace TaranSoft.MyGarage.Services.Interfaces;

public interface IGarageService
{
    public Task<IList<GarageDto>> Search(int take, int skip);
    
}