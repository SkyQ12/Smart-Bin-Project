
using SmartBin.Infrastructure.Domain.Models.Histories;

namespace SmartBin.Infrastructure.Services.BinUnits
{
    public class BinUnitService : IBinUnitService
    {
        public IBinUnitRepository _binUnitRepository {  get; set; }
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public BinUnitService(IBinUnitRepository binUnitRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _binUnitRepository = binUnitRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BinUnitViewModel> GetBinUnitById(string id)
        {
            var source = await _binUnitRepository.GetBinUnitByIdAsync(id);
            var result = _mapper.Map<BinUnit, BinUnitViewModel>(source);
            return result;
        }


        public async Task<bool> AddCollectedHistory(AddCollectedHistoryViewModel viewModel)
        {
            var source = _mapper.Map<AddCollectedHistoryViewModel, SmartBin.Infrastructure.Domain.Models.Histories.CollectedHistory>(viewModel);
            var result = _binUnitRepository.AddCollectedHistoryAsync(source);
            return await _unitOfWork.CompleteAsync();
        }
        
        public async Task<bool> AddErrorHistory(AddErrorHistoryViewModel viewModel)
        {
            var source = _mapper.Map<AddErrorHistoryViewModel, ErrorHistory>(viewModel);
            var result = _binUnitRepository.AddErrorHistoryAsync(source);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task SaveMetricsToBinUnitDatabase(string binUnitId, string metricType, object value)
        {
            await _binUnitRepository.SaveMetricsToBinUnitDatabase(binUnitId, metricType, value);
            await _unitOfWork.CompleteAsync();
        }
    }
}
