
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
            CreateMap<BinUnit, BinUnitViewModel>()
                .ForMember(dest => dest.CollectedHistories, opt => opt.MapFrom(src => src.CollectedHistories));

            CreateMap<CollectedHistory, CollectedHistoryViewModel>();
            CreateMap<ErrorHistory, ErrorHistoryViewModel>();
        }
    }
}
