
namespace SmartBin.Infrastructure.Domain.Mapping
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User, AdminViewModel>();
            CreateMap<PointChangedHistory, PointChangeHistoryViewModel>();
            CreateMap<Bin, BinViewModel>();
            CreateMap<Bin, BinForUserViewModel>();
            CreateMap<BinUnit, BinUnitViewModel>();
            CreateMap<CollectedHistory, CollectedHistoryViewModel>();
        }
    }
}
