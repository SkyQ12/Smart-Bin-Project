using Newtonsoft.Json.Linq;
using SmartBin.Infrastructure.MqttClients;
using SmartBin.Infrastructure.Repositories.ErrorHistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartBin.Infrastructure.Services.ErrorHistories
{
    public class ErrorHistoryService : IErrorHistoryService
    {
        public IErrorHistoryRepository _errorHistoryRepository { get; set; }
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public ErrorHistoryService(IErrorHistoryRepository errorHistoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _errorHistoryRepository = errorHistoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ErrorHistoryViewModel>> GetErrorHistoryByBinUnitId(string binUnitId)
        {
            var source = await _errorHistoryRepository.GetErrorHistoryByBinUnitId(binUnitId);
            var result = _mapper.Map<List<ErrorHistoryViewModel>>(source);
            return result;
        }



        public async Task<List<ErrorHistoryViewModel>> GetErrorHistoriesByDateTime(DateTime timeStamp)
        {
            var source = await _errorHistoryRepository.GetErrorHistoriesByDateTime(timeStamp);
            var result = _mapper.Map<List<ErrorHistoryViewModel>>(source);
            return result;
        }

        public async Task<List<ErrorHistoryViewModel>> GetErrorHistoriesFromDateTime1ToDateTime2(DateTime timeStamp1, DateTime timeStamp2)
        {
            var source = await _errorHistoryRepository.GetErrorHistoriesFromDateTime1ToDateTime2(timeStamp1, timeStamp2);
            var result = _mapper.Map<List<ErrorHistoryViewModel>>(source);
            return result;
        }

        public async Task<bool> DeleteErrorHistoriesByBinUnitId(string binUnitId)
        {

           var result = await _errorHistoryRepository.DeleteErrorHistoriesByBinUnitId(binUnitId);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> DeleteErrorHistoriesFromDateTime1ToDateTime2(DateTime timeStamp1, DateTime timeStamp2)
        {
            var result = await _errorHistoryRepository.DeleteErrorHistoriesFromDateTime1ToDateTime2(timeStamp1, timeStamp2);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task SaveErrorHistoryToDatabase(int id, string binUnitId, int errorId, DateTime timestamp)
        {
            await _errorHistoryRepository.SaveErrorHistoryToDatabase( id, binUnitId, errorId, timestamp);
            await _unitOfWork.CompleteAsync();
        }
    }
}
