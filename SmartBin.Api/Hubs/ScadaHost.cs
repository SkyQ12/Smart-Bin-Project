using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SmartBin.Infrastructure.Domain.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using Microsoft.Data.SqlClient;
using SmartBin.Infrastructure.MqttClients;
using SmartBin.Infrastructure.Repositories.ErrorHistories;
using SmartBin.Infrastructure.Repositories.BinUnits;
using MQTTnet.Client;
using SmartBin.Infrastructure.MqttClients;
using SmartBin.Infrastructure.Domain.Models.Bin;
namespace SmartBin.Api.Hubs
{


    public class ScadaHost : BackgroundService
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly MqttBuffer _buffer;
        public IServiceScopeFactory _serviceScopeFactory;

        public ScadaHost(ManagedMqttClient mqttClient, IHubContext<NotificationHub> notificationHub, MqttBuffer buffer, IServiceScopeFactory scopeFactory)
        {
            _mqttClient = mqttClient;
            _notificationHub = notificationHub;
            _buffer = buffer;
            _serviceScopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await ConnectToMqttBrokerAsync();
            Console.WriteLine("ScadaHost is running...");
        }


        public async Task ConnectToMqttBrokerAsync()
        {
            _mqttClient.MessageReceived += OnMqttClientMessageReceived;
            await _mqttClient.ConnectAsync();
            await _mqttClient.Subscribe("Smart_bin/Test/#");
        }


        public async Task OnMqttClientMessageReceived(MqttMessage arg)
        {
            var topic = arg.Topic;
            //Console.WriteLine(topic);
            var payloadMessage = arg.Payload;
            if (topic is null || payloadMessage is null)
            {
                return;
            }
            
            int TopicCount = topic.Count(c => c == '/');
            //Console.WriteLine($"Topic Count: {TopicCount}");


            if (TopicCount == 5)
            {

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
                    case "Battery":
                        metricType = "Battery";
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
                    //Console.WriteLine($" ID: {metric.Id}, Value: {metric.Value}, Timestamp: {metric.Timestamp}");
                    metric.BinId = binId;
                    metric.BinUnitId = binUnitId;

                    if (metric != null && metric.Id != 0)
                    {
                        //Console.WriteLine(topic);
                        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                        {
                            var binUnitService = scope.ServiceProvider.GetRequiredService<IBinUnitRepository>();
                            await binUnitService.SaveMetricsToBinUnitDatabase(binUnitId, metricType, metric.Value);
                            //Console.WriteLine("BinUnit SaveChanges called.");
                        }

                        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                        {
                            var Fault_errorHistoryService = scope.ServiceProvider.GetRequiredService<IErrorHistoryRepository>();
                            if (metricType == "Fault")
                            {
                                if (metric.Value.ToString() == "1")
                                {
                                    int errorId = 1;
                                    await Fault_errorHistoryService.SaveErrorHistoryToDatabase(metric.Id, binUnitId, errorId, metric.Timestamp);
                                }
                            }

                            var Status_errorHistoryService = scope.ServiceProvider.GetRequiredService<IErrorHistoryRepository>();
                            if (metricType == "Status")
                            {
                                if (metric.Value.ToString() == "1")
                                {
                                    int errorId = 1;
                                    await Status_errorHistoryService.SaveErrorHistoryToDatabase(metric.Id, binUnitId, errorId, metric.Timestamp);
                                }
                            }

                            var Flame_errorHistoryService = scope.ServiceProvider.GetRequiredService<IErrorHistoryRepository>();
                            if (metricType == "Flame")
                            {
                                if (metric.Value.ToString() == "1")
                                {
                                    int errorId = 1;
                                    await Flame_errorHistoryService.SaveErrorHistoryToDatabase(metric.Id, binUnitId, errorId, metric.Timestamp);
                                }
                            }

                            var Vibration_errorHistoryService = scope.ServiceProvider.GetRequiredService<IErrorHistoryRepository>();
                            if (metricType == "Vibration")
                            {
                                if (metric.Value.ToString() == "1")
                                {
                                    int errorId = 1;
                                    await Vibration_errorHistoryService.SaveErrorHistoryToDatabase(metric.Id, binUnitId, errorId, metric.Timestamp);

                                }
                            }
                        }
                    }
                }
            }

            else
            {
                string[] splitTopic = topic.Split('/');
                string unsplit_Id = splitTopic[2];
                string Id = unsplit_Id.Split('_')[1];
                string binId = "BIN" + Id;
                string metricType = "";



                switch (splitTopic[3])
                {
                    case "Battery":
                        metricType = "Battery";
                        break;
                    case "Internet":
                        metricType = "Internet";
                        break;
                    case "QR":
                        metricType = "QR";
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
                    //Console.WriteLine($" ID: {metric.Id}, Value: {metric.Value}, Timestamp: {metric.Timestamp}");
                    metric.BinId = binId;
                    if (metric != null && metric.Id != 0)
                    {
                        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                        {
                            var binUnitService = scope.ServiceProvider.GetRequiredService<IBinRepository>();
                            await binUnitService.SaveMetricsToBinDatabase(binId, metricType, metric.Value);
                            //Console.WriteLine("Bin SaveChanges called.");
                        }   
                    }
                }
            }
        }    
    }
}
