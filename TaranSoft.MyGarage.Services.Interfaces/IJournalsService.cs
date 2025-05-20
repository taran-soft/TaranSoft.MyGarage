using TaranSoft.MyGarage.Contracts.Dto;

namespace TaranSoft.MyGarage.Services.Interfaces;

public interface IJournalsService
{
    Task<ICollection<JournalDto>> GetByVehicleId(long id);
}