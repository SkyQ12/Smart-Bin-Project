using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBin.Infrastructure.Services.CollectedHistory
{
    public interface ICollectedHistoryService
    {
        public Task<bool> CreateCollectedHistory(AddCollectedHistoryViewModel collectedHistoryViewModel);
        public Task<bool> DeleteCollectedHistoriesByBinUnitId(string binUnitId);
        public Task<bool> DeleteCollectedHistoriesFromDateTime1ToDateTime2(DateTime collectedTime1, DateTime collectedTime2);
        public Task<bool> DeleteAll();
    }
}
