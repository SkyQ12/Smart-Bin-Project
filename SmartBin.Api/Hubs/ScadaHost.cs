
namespace SmartBin.Api.Hubs
{
    public class ScadaHost : BackgroundService
    {
        public ManagedMqttClient _mqttClient;
        public IHubContext<NotificationHub> _notificationHub;
        public MqttBuffer _buffer;

        public ScadaHost(ManagedMqttClient mqttClient, IHubContext<NotificationHub> notificationHub, MqttBuffer buffer)
        {
            _mqttClient = mqttClient;
            _notificationHub = notificationHub;
            _buffer = buffer;
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
             
                var json = JsonConvert.SerializeObject(metric);
                Console.WriteLine(json);

                if (metric.Name == "FullLevel")
                {
                    await _notificationHub.Clients.Group("Users").SendAsync("ReceiveForUser", json);
                }
                await _notificationHub.Clients.Group("BinAdmins").SendAsync("ReceiveForAdmin", json);

                await _buffer.Update(metric);
            }
        }
    }
}
