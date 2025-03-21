
using Microsoft.Extensions.Options;
using MQTTnet.Client;
using MQTTnet;
using System.Timers;

namespace SmartBin.Infrastructure.MqttClients
{
    public class ManagedMqttClient
    {
        public MqttOptions Options { get; set; }
        public List<string> SubscribeTopics { get; set; } = new List<string>();

        private IMqttClient? _mqttClient;
        private readonly System.Timers.Timer _reconnectTimer;

        public bool IsConnected => _mqttClient is not null && _mqttClient.IsConnected;
        public event Func<MqttMessage, Task>? MessageReceived;

        public ManagedMqttClient(IOptions<MqttOptions> mqttOptions)
        {
            Options = mqttOptions.Value;
            _reconnectTimer = new System.Timers.Timer(10000);
            _reconnectTimer.Elapsed += OnReconnectTimerElapsed;
        }

        public async Task ConnectAsync()
        {
            _reconnectTimer.Enabled = false;

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
                                        .WithKeepAlivePeriod(TimeSpan.FromSeconds(Options.KeepAliveInterval));

            _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceived;
            _mqttClient.DisconnectedAsync += OnDisconnected;

            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(Options.CommunicationTimeout));

            var result = await _mqttClient.ConnectAsync(mqttClientOptions.Build(), timeout.Token);

            if (result.ResultCode != MqttClientConnectResultCode.Success)
            {
                _reconnectTimer.Enabled = true;
            }
            else
            {
                /*foreach (var topic in SubscribeTopics)
                {
                    await _mqttClient.SubscribeAsync(topic);
                }*/
                await _mqttClient.SubscribeAsync("Smart_bin/#");
                Console.WriteLine("✅ Đã subscribe vào Smart_bin/#");

                Console.WriteLine("Connected");

                ////
                Console.WriteLine("📌 Danh sách topic đăng ký:");
                foreach (var topic in SubscribeTopics)
                {
                    Console.WriteLine($"📌 Đăng ký topic: {topic}");
                    await _mqttClient.SubscribeAsync(topic);
                }
                ////

            }
        }

        public async Task Subscribe(string topic)
        {
            if (_mqttClient is null)
            {
                throw new InvalidOperationException("MQTT Client is not connected.");
            }

            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .Build();

            var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter(topicFilter)
                .Build();

            var result = await _mqttClient.SubscribeAsync(subscribeOptions);

            foreach (var subscription in result.Items)
            {
                if (subscription.ResultCode != MqttClientSubscribeResultCode.GrantedQoS0 &&
                    subscription.ResultCode != MqttClientSubscribeResultCode.GrantedQoS1 &&
                    subscription.ResultCode != MqttClientSubscribeResultCode.GrantedQoS2)
                {
                    Console.WriteLine($"MQTT Client Subscription {subscription.TopicFilter.Topic} Failed: {subscription.ResultCode}");
                    SubscribeTopics.Add(topic);
                }
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
        private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs arg)
        {
            /*var topic = arg.ApplicationMessage.Topic;
            var payload = arg.ApplicationMessage.ConvertPayloadToString();

            if (MessageReceived is not null)
            {
                await MessageReceived(new MqttMessage(topic, payload));
            }*/

            var topic = arg.ApplicationMessage.Topic;
            var payload = arg.ApplicationMessage.ConvertPayloadToString();

            Console.WriteLine($"📩 Nhận tin nhắn - Topic: {topic} | Payload: {payload}");

            if (MessageReceived is not null)
            {
                await MessageReceived(new MqttMessage(topic, payload));
            }
        }

        public async Task Publish(string topic, string payload, bool retainFlag)
        {
            if (_mqttClient is null)
            {
                throw new InvalidOperationException("MQTT Client is not connected.");
            }

            var applicationMessageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithRetainFlag(retainFlag);

            var applicationMessage = applicationMessageBuilder.Build();

            var result = await _mqttClient.PublishAsync(applicationMessage);

            if (result.ReasonCode != MqttClientPublishReasonCode.Success)
            {
                Console.WriteLine($"MQTT Client Publish {applicationMessage.Topic} Failed: {result.ReasonCode}");
            }
        }
    }
}
