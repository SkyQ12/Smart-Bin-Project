
namespace SmartBin.Infrastructure.Services.BinUnits
{
    public interface IBinUnitService
    {
        public Task<BinUnitViewModel> GetBinUnitById(string id);
        public Task<bool> AddCollectedHistory(AddCollectedHistoryViewModel viewModel);
        public Task<bool> AddErrorHistory(AddErrorHistoryViewModel viewModel);
    }
}
