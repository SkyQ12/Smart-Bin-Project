
using System;

namespace SmartBin.Host.Hosting
{
    public class HostWorker : BackgroundService
    {
        public ManagedMqttClient _mqttClient;
        public IServiceScopeFactory _serviceScopeFactory;

        public HostWorker(ManagedMqttClient mqttClient, IServiceScopeFactory serviceScopeFactory)
        {
            _mqttClient = mqttClient;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConnectToMqttBrokerAsync();
        }

        public async Task ConnectToMqttBrokerAsync()
        {
            _mqttClient.MessageReceived += OnMqttClientMessageReceived;
            await _mqttClient.ConnectAsync();
            await _mqttClient.Subscribe("SMART_BIN/+/+");
        }
        private async Task OnMqttClientMessageReceived(MqttMessage arg)
        {
            var topic = arg.Topic;
            var payloadMessage = arg.Payload;
            if (topic is null || payloadMessage is null)
            {
                return;
            }

            string[] splitTopic = topic.Split('/');
            string binId = splitTopic[1];
            string binUnitId = "";
            switch (splitTopic[2])
            {
                case "Status":
                    binUnitId = "";
                    break;
                case "Organic":
                    binUnitId = binId + "OR";
                    break;
                case "RecyclableInorganic":
                    binUnitId = binId + "RI";
                    break;
                case "NonRecyclableInorganic":
                    binUnitId = binId + "NI";
                    break;
            }

            var metrics = JsonConvert.DeserializeObject<List<TagChangedNotification>>(payloadMessage);
            if (metrics is null)
            {
                return;
            }
            foreach (var metric in metrics)
            {
                metric.BinId = binId;
                metric.BinUnitId = binUnitId;

                if (metric.Name == "LastCollection")
                {
                    using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                    {
                        var binUnitService = scope.ServiceProvider.GetRequiredService<IBinUnitService>();
                        await binUnitService.AddCollectedHistory(new AddCollectedHistoryViewModel(binUnitId, metric.Timestamp));
                    }
                }
                else if (metric.Name == "EngineError" && metric.Value.ToString() == "True")
                {
                    using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                    {
                        var binUnitService = scope.ServiceProvider.GetRequiredService<IBinUnitService>();
                        await binUnitService.AddErrorHistory(new AddErrorHistoryViewModel(binUnitId, metric.Name, metric.Timestamp));
                    }
                }
            }
        }
    }
}
