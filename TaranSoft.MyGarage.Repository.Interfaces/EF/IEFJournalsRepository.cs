using TaranSoft.MyGarage.Data.Models.EF.CarJournal;

namespace TaranSoft.MyGarage.Repository.Interfaces.EF;

public interface IEFJournalsRepository
{
    Task<ICollection<Journal>> GetByVehicleId(long id);

    Task AddAsync(Journal journal);
    Task UpdateAsync(Journal journal);
    Task DeleteAsync(long id);
}