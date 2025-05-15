
namespace SmartBin.Infrastructure.Services.Bins
{
    public class BinService : IBinService
    {
        public IBinRepository _binRepository { get; set; }
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public BinService(IBinRepository binRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _binRepository = binRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BinViewModel>> GetAllBins()
        {
            var source = await _binRepository.GetAllBinsAsync();
            var result = _mapper.Map<List<Bin>, List<BinViewModel>>(source);
            return result;
        }

        public async Task<BinViewModel> GetBinById(string id)
        {
            var source = await _binRepository.GetBinByIdAsync(id);
            var result = _mapper.Map<Bin, BinViewModel>(source);
            return result;
        }

        public async Task<bool> DeleteBinById(string id)
        {
            var isExist = await _binRepository.GetBinByIdAsync(id);
            if (isExist is not null) 
            {
                await _binRepository.DeleteBinById(isExist);
                return await _unitOfWork.CompleteAsync();
            }
            else { return false; }  

        }

        public async Task<bool> CreateNewBin(AddNewBinViewModel viewModel)
        {
            var isExist = await _binRepository.IsBinAlreadyExist(viewModel.Id);
            if (isExist) 
            {
                return false;
            }
            else
            {
                var source = _mapper.Map<AddNewBinViewModel, Bin>(viewModel);
                await _binRepository.CreateNewBinAsync(source);

                viewModel.BinUnits.Add(new CreateNewBinUnitViewModel(viewModel.Id + "OR", viewModel.Id, BinUnitType.Organic));
                viewModel.BinUnits.Add(new CreateNewBinUnitViewModel(viewModel.Id + "RI", viewModel.Id, BinUnitType.RecyclableInorganic));
                viewModel.BinUnits.Add(new CreateNewBinUnitViewModel(viewModel.Id + "NI", viewModel.Id, BinUnitType.NonRecyclableInorganic));

                var binUnits = _mapper.Map<List<CreateNewBinUnitViewModel>, List<BinUnit>>(viewModel.BinUnits);
                await _binRepository.CreateNewBinUnitsAsync(binUnits);

                return await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> UpdateBin(string id, UpdateBinViewModel viewModel)
        {
            var isExist = await _binRepository.IsBinAlreadyExist(id);
            if (!isExist)
            {
                return false;
            }
            else 
            {
                var bin = await _binRepository.GetBinByIdAsync(id);
                if (!string.IsNullOrEmpty(viewModel.Longtitude.ToString())) 
                {
                    bin.Longtitude = viewModel.Longtitude;
                }
                if (!string.IsNullOrEmpty(viewModel.Latitude.ToString()))
                {
                    bin.Latitude = viewModel.Latitude;
                }
                if (!string.IsNullOrEmpty(viewModel.Address))
                {
                    bin.Address = viewModel.Address;
                }
                await _binRepository.UpdateBinAsync(bin);
                return await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<List<BinForUserViewModel>> GetBinsForUser()
        {
            var source = await _binRepository.GetAllBinsAsync();
            var result = _mapper.Map<List<Bin>, List<BinForUserViewModel>>(source);
            return result;
        }

        
        public async Task<bool> DeleteQRByBinId(string binId)
        {
            await _binRepository.DeleteQRByBinId(binId);
            return await _unitOfWork.CompleteAsync();
        }
        
        public async Task<bool> DeleteQRByQR(string qR, string binId)
        {
            await _binRepository.DeleteQRByQR(qR, binId);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task SaveMetricsToBinDatabase(string binId, string metricType, object value)
        {
            await _binRepository.SaveMetricsToBinDatabase(binId, metricType, value);
            await _unitOfWork.CompleteAsync();
        }
    }
}
