using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Simulation_Publishers.Mqtt;
using Simulation_Publishers.Models;

namespace Simulation_Publishers.Mqtt
{
    public class SensorPublisher
    {
        private readonly MqttClient _mqttClient;
        private readonly List<MqttControl> _bins;

        public SensorPublisher(MqttClient mqttClient, List<MqttControl> bins)
        {
            _mqttClient = mqttClient;
            _bins = bins;
        }

        // Hàm khởi động việc publish dữ liệu của tất cả các bin với hỗ trợ hủy bỏ
        public async Task StartPublishingDataAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested) // Kiểm tra nếu có yêu cầu hủy
            {
                Console.WriteLine("[Info] ⏰ Preparing to publish sensor data...");

                // Chạy vòng lặp cho mỗi bin để gửi dữ liệu
                foreach (var bin in _bins)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    await bin.PrepareForPublish(cancellationToken); // Truyền cancellationToken vào đây
                }

                // Tạm dừng một chút trước khi lặp lại lần tiếp theo
                await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
            }
        }
    }
}
