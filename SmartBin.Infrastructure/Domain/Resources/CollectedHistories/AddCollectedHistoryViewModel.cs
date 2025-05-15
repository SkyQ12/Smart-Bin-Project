
namespace SmartBin.Infrastructure.Domain.Resources.CollectedHistories
{
    [DataContract]
    public class AddCollectedHistoryViewModel
    {
        [DataMember]
        public string BinId { get; set; }

        [DataMember]
        public string? BinType { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string District { get; set; }

        [DataMember]
        public DateTime CollectedTime { get; set; }

        [IgnoreDataMember]
        public string BinUnitId => BinId + (BinType switch
        {
            "Thực phẩm" => "OR",
            "Tái chế" => "RI",
            "Không tái chế" => "NI",
            _ => ""
        });

        public AddCollectedHistoryViewModel(string binId, string? binType, string street, string district, DateTime collectedTime)
        {
            BinId = binId;
            BinType = binType;
            Street = street;
            District = district;
            CollectedTime = collectedTime;
        }
    }

}
