
namespace SmartBin.Infrastructure.Domain.Resources.Bins
{
    [DataContract]
    public class AddNewBinViewModel
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public double Longtitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public string Address { get; set; }
        [JsonIgnore]
        [DataMember]
        public List<CreateNewBinUnitViewModel> BinUnits { get; set; } 

        public AddNewBinViewModel(string id, double longtitude, double latitude, string address)
        {
            Id = id;
            Longtitude = longtitude;
            Latitude = latitude;
            Address = address;
            BinUnits = new List<CreateNewBinUnitViewModel>();
        }
    }
}
