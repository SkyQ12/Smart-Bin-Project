
namespace SimulationBins.MqttClient
{
    public class MqttMessage
    {
        public string? Topic { get; set; }
        public string? Payload { get; set; }
        public MqttMessage(string topic, string payload)
        {
            Topic = topic;
            Payload = payload;
        }
    }
}
