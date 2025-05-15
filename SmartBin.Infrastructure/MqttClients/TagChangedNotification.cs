
namespace SmartBin.Infrastructure.MqttClients
{
    public class TagChangedNotification
    {
        public string BinId {  get; set; }
        public string BinUnitId { get; set; }
        public int Id { get; set; }
        public object Value { get; set; }
        public DateTime Timestamp { get; set; }
        public MetricType Type { get; set; }
        public TagChangedNotification(string binId, string binUnitId, int id, object value, DateTime timestamp)
        {
            BinId = binId;
            BinUnitId = binUnitId;
            Id = id;
            Value = value;
            Timestamp = timestamp;
        }
    }
}
