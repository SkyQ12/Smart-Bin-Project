
using SmartBin.Infrastructure.Repositories.CollectedHistories;
using SmartBin.Infrastructure.Domain.Resources.CollectedHistories;
using SmartBin.Infrastructure.Domain.Models.Histories;

namespace SmartBin.Infrastructure.Services.CollectedHistory
{
    public class CollectedHistoryService : ICollectedHistoryService
    {
        public ICollectedHistoryRepository _collectedHistoryRepository { get; set; }
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public CollectedHistoryService(ICollectedHistoryRepository collectedHistoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _collectedHistoryRepository = collectedHistoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateCollectedHistory(AddCollectedHistoryViewModel collectedHistoryViewModel)
        {
            var source = _mapper.Map<AddCollectedHistoryViewModel, SmartBin.Infrastructure.Domain.Models.Histories.CollectedHistory>(collectedHistoryViewModel);
            await _collectedHistoryRepository.CreateCollectedHistory(source);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<bool> DeleteCollectedHistoriesByBinUnitId(string binUnitId)
        {

            var result = await _collectedHistoryRepository.DeleteCollectedHistoriesByBinUnitId(binUnitId);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> DeleteCollectedHistoriesFromDateTime1ToDateTime2(DateTime collectedTime1, DateTime collectedTime2)
        {
            var result = await _collectedHistoryRepository.DeleteCollectedHistoriesFromDateTime1ToDateTime2(collectedTime1, collectedTime2);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        public async Task<bool> DeleteAll()
        {
            var result = await _collectedHistoryRepository.DeleteAll();
            await _unitOfWork.CompleteAsync();
            return result;
        }
    }
}
