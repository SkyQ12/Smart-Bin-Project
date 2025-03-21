
namespace SmartBin.Infrastructure.Repositories.BinUnits
{
    public class BinUnitRepository : BaseRepository, IBinUnitRepository
    {
        public BinUnitRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddCollectedHistoryAsync(CollectedHistory history)
        {
            await _context.CollectedHistories.AddAsync(history);
        }

        public async Task AddErrorHistoryAsync(ErrorHistory history)
        {
            await _context.ErrorHistories.AddAsync(history);
        }

        public async Task<BinUnit> GetBinUnitByIdAsync(string id)
        {
            var binUnit = await _context.BinUnits.Include(x => x.CollectedHistories).FirstOrDefaultAsync(x => x.BinUnitId == id);
            return binUnit != null ? binUnit : throw new ResourceNotfoundException("Not found binUnit");
        }

        public async Task<bool> IsExistBinUnit(string id)
        {
            return await _context.BinUnits.AnyAsync(x => x.BinUnitId == id);
        }
    }
}
