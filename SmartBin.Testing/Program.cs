using MQTTnet.Client;
using MQTTnet;
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var options = new MqttClientOptionsBuilder()
                            .WithClientId(string.Empty)
                            .WithTcpServer("20.41.104.186", 1883)
                            .WithTimeout(TimeSpan.FromSeconds(30))
                            .WithKeepAlivePeriod(TimeSpan.FromSeconds(60))
                            .Build();

        var mqttClient = new MqttFactory().CreateMqttClient();
        await mqttClient.ConnectAsync(options);

        Console.WriteLine("Connected MQTT broker!");
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.A)
            {
                //Mô phỏng cập nhật dữ liệu
                for (int i = 1; i <= 10; i++)
                {
                    string topic = "SMART_BIN/BIN" + i.ToString("00") + "/Status";
                    string payload = StatusPayloadMessage();
                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic(topic)
                        .WithPayload(payload)
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                        .WithRetainFlag(true)
                        .Build();

                    await mqttClient.PublishAsync(message);
                    Console.WriteLine(topic + " " + payload);
                }
            }
            else if (keyInfo.Key == ConsoleKey.S)
            {
                for (int i = 1; i <= 10; i++)
                {
                    string topic = "SMART_BIN/BIN" + i.ToString("00") + "/Organic";
                    string payload = BinUnitPayloadMessage();
                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic(topic)
                        .WithPayload(payload)
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                        .WithRetainFlag(true)
                        .Build();

                    await mqttClient.PublishAsync(message);
                    Console.WriteLine(topic + " " + payload);
                }
            }
            else if (keyInfo.Key == ConsoleKey.D)
            {
                for (int i = 1; i <= 10; i++)
                {
                    string topic = "SMART_BIN/BIN" + i.ToString("00") + "/RecyclableInorganic";
                    string payload = BinUnitPayloadMessage();
                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic(topic)
                        .WithPayload(payload)
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                        .WithRetainFlag(true)
                        .Build();

                    await mqttClient.PublishAsync(message);
                    Console.WriteLine(topic + " " + payload);
                }
            }
            else if (keyInfo.Key == ConsoleKey.F)
            {
                for (int i = 1; i <= 10; i++)
                {
                    string topic = "SMART_BIN/BIN" + i.ToString("00") + "/NonRecyclableInorganic";
                    string payload = BinUnitPayloadMessage();
                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic(topic)
                        .WithPayload(payload)
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                        .WithRetainFlag(true)
                        .Build();

                    await mqttClient.PublishAsync(message);
                    Console.WriteLine(topic + " " + payload);
                }
            }
        }
    }
    static string StatusPayloadMessage()
    {
        Random random = new Random();

        double battery = random.Next(0, 100) + random.NextDouble();
        string isConnected = "False";

        var sensorDataList = new List<PayloadData>
        {
            new PayloadData {name = "Battery",value = battery.ToString("F2"),timestamp = DateTime.Now.ToString("HH:mm:ss")},
            new PayloadData {name = "Connected",value = isConnected.ToString(),timestamp = DateTime.Now.ToString("HH:mm:ss")}
        };

        string jsonPayload = JsonConvert.SerializeObject(sensorDataList);
        return jsonPayload;

    }
    static string BinUnitPayloadMessage()
    {
        Random random = new Random();

        string fullLevel = random.Next(0, 100).ToString();
        string engineError = "False";
        var lastcollection = DateTime.Now.AddHours(-1);
        bool isConnected = false;

        var sensorDataList = new List<PayloadData>
        {
            new PayloadData {name = "FullLevel",value = fullLevel,timestamp = DateTime.Now.ToString("HH:mm:ss")},
            new PayloadData {name = "EngineError",value = engineError,timestamp = DateTime.Now.ToString("HH:mm:ss")},
            new PayloadData {name = "LastCollection",value = lastcollection.ToString(),timestamp = DateTime.Now.ToString("HH:mm:ss")},
            new PayloadData {name = "IsConnected",value = isConnected.ToString(),timestamp = DateTime.Now.ToString("HH:mm:ss")}
        };

        string jsonPayload = JsonConvert.SerializeObject(sensorDataList);
        return jsonPayload;

    }
}
public class PayloadData
{
    public string name { get; set; } = "";
    public string value { get; set; } = "";
    public string timestamp { get; set; } = "";
}
