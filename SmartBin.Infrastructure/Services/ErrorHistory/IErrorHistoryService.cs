using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBin.Infrastructure.Services.ErrorHistories
{
    public interface IErrorHistoryService
    {
        public Task<List<ErrorHistoryViewModel>> GetErrorHistoryByBinUnitId(string binUnitId);
        public Task<List<ErrorHistoryViewModel>> GetErrorHistoriesByDateTime(DateTime timeStamp);
        public Task<List<ErrorHistoryViewModel>> GetErrorHistoriesFromDateTime1ToDateTime2(DateTime timeStamp1, DateTime timeStamp2);
        public Task<bool> DeleteErrorHistoriesByBinUnitId(string binUnitId);
        public Task<bool> DeleteErrorHistoriesFromDateTime1ToDateTime2(DateTime timeStamp1, DateTime timeStamp2);
        public Task SaveErrorHistoryToDatabase(int id, string binUnitId, int errorId, DateTime timeStamp);

    }
}
