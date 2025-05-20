using Microsoft.EntityFrameworkCore;
using TaranSoft.MyGarage.Data.Models.EF.CarJournal;
using TaranSoft.MyGarage.Repository.Interfaces.EF;

namespace TaranSoft.MyGarage.Repository.EntityFramework;

public class JournalsRepository : BaseRepository<Journal>, IEFJournalsRepository
{
    private readonly MainDbContext _context;

    public JournalsRepository(MainDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<Journal>> GetByVehicleId(long id)
    {
        return await _context.Journals.Where(j => j.VehicleId == id).ToListAsync();
    }

    public async Task AddAsync(Journal journal)
    {
        _context.Journals.Add(journal);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Journal journal)
    {
        _context.Journals.Update(journal);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var journal = await _context.Journals.FindAsync(id);
        if (journal != null)
        {
            _context.Journals.Remove(journal);
            await _context.SaveChangesAsync();
        }
    }
}