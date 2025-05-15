
using System;
using System.Collections.Generic;
using SmartBin.Infrastructure.Repositories.BinUnits;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            await _mqttClient.Subscribe("Smart_bin/Test/+/+/+/+");
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
            string unsplit_Id = splitTopic[3];
            string Id = unsplit_Id.Split('_')[1];
            string binId = "BIN" + Id;
            string binUnitId = "";
            string metricType = "";

            switch (unsplit_Id.Split('_')[2])
            {
                case "Food":
                    binUnitId = binId + "OR";
                    break;
                case "Recycle":
                    binUnitId = binId + "RI";
                    break;
                case "Other":
                    binUnitId = binId + "NI";
                    break;
            }

            switch (splitTopic[5])
            {
                case "Level":
                    metricType = "Level";
                    break;
                case "Fault":
                    metricType = "Fault";
                    break;
                case "Compress_cnt":
                    metricType = "CompressCnt";
                    break;
                case "Full_cnt":
                    metricType = "FullCnt";
                    break;
                case "Status":
                    metricType = "Status";
                    break;
                case "Flame":
                    metricType = "Flame";
                    break;
                case "Vibration":
                    metricType = "Vibration";
                    break;
            }

            List<TagChangedNotification> metrics;
            if (payloadMessage.Trim().StartsWith("{"))
            {
                // Nếu payload là một object đơn
                var singleMetric = JsonConvert.DeserializeObject<TagChangedNotification>(payloadMessage);
                metrics = new List<TagChangedNotification> { singleMetric };
            }
            else
            {
                // Nếu payload là một danh sách JSON
                metrics = JsonConvert.DeserializeObject<List<TagChangedNotification>>(payloadMessage);
            }


            foreach (var metric in metrics)
            {
                Console.WriteLine($" ID: {metric.Id}, Value: {metric.Value}, Timestamp: {metric.Timestamp}");
                metric.BinId = binId;
                metric.BinUnitId = binUnitId;

                if (metric != null && metric.Id != 0)
                {
                    using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                    {
                        var binUnitService = scope.ServiceProvider.GetRequiredService<IBinUnitRepository>();
                        await binUnitService.SaveMetricsToBinUnitDatabase(binUnitId, metricType, metric.Value);
                    }
                }
            }

            /*
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
            */

        }
    }
}
