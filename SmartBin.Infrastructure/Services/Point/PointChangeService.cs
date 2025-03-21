

namespace SmartBin.Infrastructure.Services.Point
{
    public class PointChangeService : IPointChangeService
    {
        public IPointChangeRepository _pointChangeRepository { get; set; }
        public IUserRepository _userRepository { get; set; }
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public PointChangeService(IPointChangeRepository pointChangeRepository,IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _pointChangeRepository = pointChangeRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UpdatePoint(string id, UpdatePointViewModel newPoint)
        {
            var isExist = await _userRepository.GetUserByIdAsync(id);
            var oldPoint = isExist.Point;
            isExist.Point = newPoint.Point;
            await _pointChangeRepository.UpdatePointAsync(isExist);

            var history = new AddPointChangedHistoryViewModel(id, oldPoint, newPoint.Point, DateTime.UtcNow.AddHours(7));
            var source = _mapper.Map<AddPointChangedHistoryViewModel, PointChangedHistory>(history);
            await _pointChangeRepository.AddPointChangedHistory(source);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<List<PointChangeHistoryViewModel>> GetPointChangedHistory(string userId)
        {
            var source = await _pointChangeRepository.GetPointsChangedHistoryAsync(userId);
            var result = _mapper.Map<List<PointChangedHistory>, List<PointChangeHistoryViewModel>>(source);
            return result;
        }
    }
}
