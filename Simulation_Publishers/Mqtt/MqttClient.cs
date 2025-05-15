using Microsoft.Extensions.Options;
using MQTTnet.Client;
using MQTTnet;
using System.Timers;

namespace Simulation_Publishers.Mqtt
{

    public class MqttClient
    {
        public MqttOptions Options { get; set; }
        public List<string> SubscribeTopics { get; set; } = new List<string>();

        public IMqttClient? _mqttClient;
        private readonly System.Timers.Timer _reconnectTimer;

        public bool IsConnected => _mqttClient is not null && _mqttClient.IsConnected;

        public MqttClient(IOptions<MqttOptions> mqttOptions)
        {
            Options = mqttOptions.Value;
            if (string.IsNullOrEmpty(Options.Host))
            {
                throw new ArgumentException("Host is not configured correctly in appsettings.json", nameof(Options.Host));
            }
            _reconnectTimer = new System.Timers.Timer(10000);
            _reconnectTimer.Elapsed += OnReconnectTimerElapsed;
        }

        public async Task ConnectAsync()
        {
            _reconnectTimer.Enabled = false;

            if (_mqttClient != null && _mqttClient.IsConnected)
            {
                Console.WriteLine("Client is already connected.");
                return;
            }

            if (_mqttClient != null)
            {
                await _mqttClient.DisconnectAsync();
                _mqttClient.Dispose();
            }

            _mqttClient = new MqttFactory().CreateMqttClient();

            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(Options.Host, Options.Port)
                .WithTimeout(TimeSpan.FromSeconds(Options.CommunicationTimeout))
                .WithKeepAlivePeriod(TimeSpan.FromSeconds(Options.KeepAliveInterval));

            _mqttClient.DisconnectedAsync += OnDisconnected;

            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(Options.CommunicationTimeout));

            var result = await _mqttClient.ConnectAsync(mqttClientOptions.Build(), timeout.Token);

            if (result.ResultCode != MqttClientConnectResultCode.Success)
            {
                _reconnectTimer.Enabled = true;
            }
            else
            {
                // Kết nối thành công, có thể tiếp tục publish
                foreach (var topic in SubscribeTopics)
                {
                    await _mqttClient.SubscribeAsync(topic);
                }
                Console.WriteLine("Connected");
            }
        }


        public async Task OnDisconnected(MqttClientDisconnectedEventArgs arg)
        {
            await ConnectAsync();
        }
        public async void OnReconnectTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            await ConnectAsync();
        }


        public async Task Publish(string topic, string payload)
        {
            if (_mqttClient == null)
            {
                Console.WriteLine("MQTT Client is null");
                throw new InvalidOperationException("MQTT Client is not initialized.");
            }

            if (!_mqttClient.IsConnected)
            {
                Console.WriteLine("MQTT Client is not connected.");
                await ConnectAsync();  // Thử kết nối lại
            }

            if (_mqttClient.IsConnected)
            {
                var applicationMessageBuilder = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(payload)
                    .WithRetainFlag(true);

                var applicationMessage = applicationMessageBuilder.Build();

                var result = await _mqttClient.PublishAsync(applicationMessage);

                if (result.ReasonCode != MqttClientPublishReasonCode.Success)
                {
                    Console.WriteLine($"MQTT Client Publish {applicationMessage.Topic} Failed: {result.ReasonCode}");
                }
                else
                {
                    Console.WriteLine($"Message published to {topic} with payload: {payload}");
                }
            }
            else
            {
                Console.WriteLine("MQTT Client is still not connected after retry.");
            }
        }
    }
}
