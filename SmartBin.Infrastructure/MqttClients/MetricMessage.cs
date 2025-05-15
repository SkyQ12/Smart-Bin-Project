
namespace SmartBin.Infrastructure.MqttClients
{
    public class MetricMessage
    {
        public string Id { get; set; }
        public object Value { get; set; }
        public DateTime Timestamp { get; set; }

        public MetricMessage(string id, object value, DateTime timestamp)
        {
            Id = id;
            Value = value;
            Timestamp = timestamp;
        }
    }
}
