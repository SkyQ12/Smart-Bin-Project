
namespace SmartBin.Infrastructure.Repositories.Bins
{
    public class BinRepository : BaseRepository, IBinRepository
    {
        public BinRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Bin> CreateNewBinAsync(Bin bin)
        {
            var newBin = await _context.Bins.AddAsync(bin);
            return newBin.Entity;
        }
        public async Task CreateNewBinUnitsAsync(List<BinUnit> binUnits)
        {
            await _context.BinUnits.AddRangeAsync(binUnits);
        }

        public async Task DeleteBinById(Bin bin)
        {
            _context.Bins.Remove(bin);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bin>> GetAllBinsAsync()
        {
            return await _context.Bins.Include(x => x.BinUnits).ThenInclude(x => x.CollectedHistories).ToListAsync();
        }

        public async Task<Bin> GetBinByIdAsync(string id)
        {
            var bin = await _context.Bins.Include(x => x.BinUnits).FirstOrDefaultAsync(x => x.Id == id);
            return bin != null ? bin : throw new ResourceNotfoundException("Not found bin!");
        }

        public async Task<bool> IsBinAlreadyExist(string id)
        {
            return await _context.Bins.AnyAsync(x => x.Id == id);
        }

        public async Task UpdateBinAsync(Bin bin)
        {
            _context.Bins.Update(bin);
            await _context.SaveChangesAsync();
        }
    }
}
