
namespace SmartBin.Infrastructure.Repositories.BinUnits
{
    public interface IBinUnitRepository
    {
        public Task<BinUnit> GetBinUnitByIdAsync(string id);
        public Task<bool> IsExistBinUnit(string id);
        public Task AddCollectedHistoryAsync(CollectedHistory history); 
        public Task AddErrorHistoryAsync(ErrorHistory history);
    }
}
