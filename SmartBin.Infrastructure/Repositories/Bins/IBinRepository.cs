﻿
namespace SmartBin.Infrastructure.Repositories.Bins
{
    public interface IBinRepository
    {
        public Task<List<Bin>> GetAllBinsAsync();
        public Task<Bin> GetBinByIdAsync(string id);
        public Task DeleteBinById (Bin bin);
        public Task<Bin> CreateNewBinAsync(Bin bin);
        public Task CreateNewBinUnitsAsync(List<BinUnit> binUnits);
        public Task<bool> IsBinAlreadyExist(string id);
        public Task UpdateBinAsync(Bin bin);
        public Task DeleteQRByBinId(string binId);
        public Task DeleteQRByQR(string qR,string binId);
        public Task SaveMetricsToBinDatabase(string binId, string metricType, object value);
    }
}
