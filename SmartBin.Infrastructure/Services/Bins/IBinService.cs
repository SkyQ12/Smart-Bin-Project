
namespace SmartBin.Infrastructure.Services.Bins
{
    public interface IBinService
    {
        public Task<List<BinViewModel>> GetAllBins();
        public Task<BinViewModel> GetBinById(string id);
        public Task<bool> DeleteBinById(string id);
        public Task<bool> CreateNewBin(AddNewBinViewModel viewModel);
        public Task<bool> UpdateBin(string id, UpdateBinViewModel viewModel);
        public Task SaveMetricsToBinDatabase(string binId, string metricType, object value);
        public Task<bool> DeleteQRByBinId(string binId);
        public Task<bool> DeleteQRByQR(string qR, string binId);
        public Task<List<BinForUserViewModel>> GetBinsForUser();
    }
}
