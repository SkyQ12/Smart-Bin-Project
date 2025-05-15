using SimulationBins.MqttClient;
using System;
using System.Text.Json;
using System.Threading.Tasks;
namespace SimulationBins.Control
{


    public class DataPublisher
    {
        private readonly MqttManaged _mqttManaged;

        public DataPublisher(MqttManaged mqttManaged)
        {
            _mqttManaged = mqttManaged;
        }

        public async Task PublishData(string topic, string value, string timestamp)
        {
            // Tạo dữ liệu cần gửi
            int MetricId;
            switch(topic.Split('/')[5])
            {
                case "Fault":
                    MetricId = 2;
                    break;
                case "Level":
                    MetricId = 3;
                    break;
                case "Compress_cnt":
                    MetricId = 4;
                    break;
                case "Full_cnt":
                    MetricId = 5;
                    break;
                case "Status":
                    MetricId = 6;
                    break;
                case "Flame":
                    MetricId = 7;
                    break;
                case "Vibration":
                    MetricId = 8;
                    break;
                default:
                    MetricId = 0;
                    break;
            }
            var data = new
            {
                id = MetricId,  // Ví dụ ID là 3 (Level)
                value = value,
                timestamp = timestamp
            };

            // Chuyển dữ liệu thành JSON
            string payload = JsonSerializer.Serialize(data);

            // Gửi dữ liệu qua MQTT
            await _mqttManaged.Publish(topic, payload);
        }
    }
}

