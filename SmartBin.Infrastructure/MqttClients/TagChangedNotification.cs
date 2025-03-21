
namespace SmartBin.Infrastructure.MqttClients
{
    public class TagChangedNotification
    {
        public string BinId {  get; set; }
        public string BinUnitId { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public DateTime Timestamp { get; set; }

        public TagChangedNotification(string binId, string binUnitId, string name, object value, DateTime timestamp)
        {
            BinId = binId;
            BinUnitId = binUnitId;
            Name = name;
            Value = value;
            Timestamp = timestamp;
        }
    }
}
