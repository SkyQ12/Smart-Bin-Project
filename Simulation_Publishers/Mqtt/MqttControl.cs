using Simulation_Publishers.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Simulation_Publishers.Mqtt
{
    public abstract class MqttControl : IMqttControl
    {
        private static readonly object _lock = new();
        private static readonly List<(string topic, Func<object> valueProvider, int sensorId)> _publishers = new();
        private static bool _isInitialized = false;
        private static Timer? _sharedTimer;

        public readonly MqttClient _mqttClient;

        public string BinUnitId { get; protected set; }

        static public void ClearPublishers()
        {
            lock (_lock)
            {
                _publishers.Clear();
                _isInitialized = false;

                if (_sharedTimer != null)
                {
                    _sharedTimer.Dispose();
                    _sharedTimer = null;
                }
            }
        }

        public MqttControl(MqttClient mqttClient)
        {
            _mqttClient = mqttClient;
        }

        public abstract void StartSimulation();

        protected void StartSensorPublisher(int sensorId, Func<object> valueProvider)
        {
            string MetricType;

            switch (sensorId)
            {
                case 1:
                    MetricType = "QR";
                    break;
                case 2:
                    MetricType = "Fault";
                    break;
                case 3:
                    MetricType = "Level";
                    break;
                case 4:
                    MetricType = "Compress_cnt";
                    break;
                case 5:
                    MetricType = "Full_cnt";
                    break;
                case 6:
                    MetricType = "Status";
                    break;
                case 7:
                    MetricType = "Flame";
                    break;
                case 8:
                    MetricType = "Vibration";
                    break;
                case 9:
                    MetricType = "Battery";
                    break;
                default:
                    MetricType = null;
                    break;
            }

            var topic = TopicCreate.BuildTopic(BinUnitId, MetricType);

            lock (_lock)
            {
                _publishers.Add((topic, valueProvider, sensorId));

                if (!_isInitialized)
                {
                    _isInitialized = true;
                    _ = Task.Run(RunPublishLoop);
                }
            }
        }

        protected async Task RunPublishLoop()
        {
            while (true)
            {
                await PrepareForPublish(CancellationToken.None); // Pass cancellationToken as required
            }
        }

        public async Task PrepareForPublish(CancellationToken cancellationToken)
        {
            List<(string topic, Func<object> valueProvider, int sensorId)> currentPublishers;

            lock (_lock)
            {
                currentPublishers = new List<(string, Func<object>, int)>(_publishers);
            }

            Console.WriteLine($"[Info] 📤 Start publishing {currentPublishers.Count} sensors sequentially...");

            foreach (var (topic, valueProvider, sensorId) in currentPublishers)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var value = valueProvider();
                var payload = new PayloadMessage(sensorId.ToString(), value, DateTime.UtcNow);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                await _mqttClient.Publish(topic, json);

                Console.WriteLine($"[Info] ✅ Sent sensor {sensorId} to topic {topic}");
                await Task.Delay(200); // small delay to avoid congestion
            }

            Console.WriteLine($"[Info] ✅ All sensors published.");
        }
    }
}
