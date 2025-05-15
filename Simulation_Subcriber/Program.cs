using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace Subscriber;
public class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("-- MQTT Subscriber --");
        var mqttFactory = new MqttFactory();
        using var mqttClient = mqttFactory.CreateMqttClient();
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("broker.hivemq.com", 1883)
            .WithCleanSession()
            .Build();



        mqttClient.ApplicationMessageReceivedAsync += OnMessageReceived;

        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter(
                f => {
                    f.WithTopic("Smart_bin/Test/+/+/+/+");
                })
            .Build();

        await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

        Console.WriteLine("MQTT client subscribed to topic.");
        Thread.Sleep(-1);
    }

    static async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs e)
    {
        Console.WriteLine($"# Received message: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
        await Task.CompletedTask;
    }
}