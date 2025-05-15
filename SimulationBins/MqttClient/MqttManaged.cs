using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using System.Timers;

namespace SimulationBins.MqttClient
{
    public class MqttManaged
    {
        public MqttOptions Options { get; set; }
        public List<string> SubscribeTopics { get; set; } = new List<string>();

        private IMqttClient? _mqttClient;
        private readonly System.Timers.Timer _reconnectTimer;

        public bool IsConnected => _mqttClient is not null && _mqttClient.IsConnected;
        public event Func<MqttMessage, Task>? MessageReceived;

        public MqttManaged(IOptions<MqttOptions> mqttOptions)
        {
            Options = mqttOptions.Value;
            _reconnectTimer = new System.Timers.Timer(10000); // 10s retry
            _reconnectTimer.Elapsed += OnReconnectTimerElapsed;
        }

        public async Task ConnectAsync()
        {
            _reconnectTimer.Enabled = false;

            try
            {
                if (_mqttClient is not null)
                {
                    await _mqttClient.DisconnectAsync();
                    _mqttClient.Dispose();
                }

                _mqttClient = new MqttFactory().CreateMqttClient();

                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(Options.Host, Options.Port)
                    .WithTimeout(TimeSpan.FromSeconds(Options.CommunicationTimeout))
                    .WithClientId(Options.ClientId)
                    .WithCredentials(Options.Username, Options.Password)
                    .WithKeepAlivePeriod(TimeSpan.FromSeconds(Options.KeepAliveInterval))
                    .Build();

                _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceived;
                _mqttClient.DisconnectedAsync += OnDisconnected;

                using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(Options.CommunicationTimeout));

                Console.WriteLine("Trying to connect MQTT...");

                var result = await _mqttClient.ConnectAsync(mqttClientOptions, timeout.Token);

                if (result.ResultCode == MqttClientConnectResultCode.Success)
                {
                    Console.WriteLine("MQTT connected successfully!");

                    foreach (var topic in SubscribeTopics)
                    {
                        await _mqttClient.SubscribeAsync(topic);
                        Console.WriteLine($"Subscribed to topic: {topic}");
                    }
                }
                else
                {
                    Console.WriteLine($"MQTT connection failed: {result.ResultCode}");
                    _reconnectTimer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during MQTT connect: {ex.Message}");
                _reconnectTimer.Enabled = true;
            }
        }

        public async Task OnDisconnected(MqttClientDisconnectedEventArgs arg)
        {
            Console.WriteLine("MQTT disconnected. Attempting to reconnect...");
            _reconnectTimer.Enabled = true;
        }

        private async void OnReconnectTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            await ConnectAsync();
        }

        private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs arg)
        {
            var topic = arg.ApplicationMessage.Topic;
            var payload = arg.ApplicationMessage.ConvertPayloadToString();

            if (MessageReceived is not null)
            {
                await MessageReceived(new MqttMessage(topic, payload));
            }
        }

        public async Task Publish(string topic, string payload)
        {
            if (_mqttClient is null || !_mqttClient.IsConnected)
            {
                Console.WriteLine("Publish failed: MQTT Client is not connected.");
                return;
            }

            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithRetainFlag(true)
                .Build();

            var result = await _mqttClient.PublishAsync(applicationMessage);

            if (result.ReasonCode != MqttClientPublishReasonCode.Success)
            {
                Console.WriteLine($"MQTT publish to {topic} failed: {result.ReasonCode}");
            }
            else
            {
                Console.WriteLine($"Published to {topic}: {payload}");
            }
        }
    }
}
