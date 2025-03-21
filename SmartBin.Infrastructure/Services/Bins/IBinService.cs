
namespace SmartBin.Infrastructure.Services.Bins
{
    public interface IBinService
    {
        public Task<List<BinViewModel>> GetAllBins();
        public Task<BinViewModel> GetBinById(string id);
        public Task<bool> DeleteBinById(string id);
        public Task<bool> CreateNewBin(AddNewBinViewModel viewModel);
        public Task<bool> UpdateBin(string id, UpdateBinViewModel viewModel);
        public Task<List<BinForUserViewModel>> GetBinsForUser();
    }
}
