using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBin.Infrastructure.Repositories.CollectedHistories
{
    public interface ICollectedHistoryRepository
    {
        public Task UpdateCollectedHistoryAsync(CollectedHistory collectedHistory);
        
        public Task<CollectedHistory> CreateCollectedHistory(CollectedHistory historyEntity);
        public Task<bool> DeleteCollectedHistoriesByBinUnitId(string binUnitId);
        public Task<bool> DeleteCollectedHistoriesFromDateTime1ToDateTime2(DateTime collectedTime1, DateTime collectedTime2);
        public Task<bool> DeleteAll();
    }
}
