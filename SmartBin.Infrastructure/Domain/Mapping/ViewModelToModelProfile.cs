
namespace SmartBin.Infrastructure.Domain.Mapping
{
    public class ViewModelToModelProfile : Profile
    {
        public ViewModelToModelProfile()
        {
            CreateMap<CreateNewUserViewModel, User>();
            CreateMap<AddPointChangedHistoryViewModel, PointChangedHistory>();
            CreateMap<AddNewBinViewModel, Bin>();
            CreateMap<CreateNewBinUnitViewModel, BinUnit>();
            CreateMap<AddCollectedHistoryViewModel, CollectedHistory>();
            CreateMap<AddErrorHistoryViewModel, ErrorHistory>();
            CreateMap<RegisterUserAdminViewModel, User>();
            CreateMap<RegisterWorkerAdminViewModel, User>();
            CreateMap<RegisterBinAdminViewModel, User>();
        }
    }
}
