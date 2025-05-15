/*
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

public class MqttBackgroundService : BackgroundService
{
    private readonly ILogger<MqttBackgroundService> _logger;
    private readonly IConfiguration _configuration;
    private IMqttClient _mqttClient;

    public MqttBackgroundService(ILogger<MqttBackgroundService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var mqttFactory = new MqttFactory();
        _mqttClient = mqttFactory.CreateMqttClient();

        var mqttOptions = new MqttClientOptionsBuilder()
            .WithClientId(Guid.NewGuid().ToString())
            .WithTcpServer("20.41.104.186", 1883)
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(60))
            .Build();

        _mqttClient.ApplicationMessageReceivedAsync += async e =>
        {
            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            try
            {
                if (payload.Contains("\"name\""))
                {
                    return;
                }

                var data = JsonConvert.DeserializeObject<PayloadData>(payload);
                if (data != null)
                {
                    _logger.LogInformation($"[✔] Value: {data.Value}");

                    // Lưu vào database
                    await SaveToDatabase(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"⚠ Lỗi khi xử lý dữ liệu: {ex.Message}");
            }
        };

        await _mqttClient.ConnectAsync(mqttOptions, stoppingToken);
        _logger.LogInformation("[✔] Connected to MQTT broker!");

        string topic = "Smart_bin/Test/Bin_4/Bin_4_Food/Metric/Level";
        await _mqttClient.SubscribeAsync(topic);
        _logger.LogInformation($"[✔] Subscribed to topic: {topic}");
    }

    private async Task SaveToDatabase(PayloadData data)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (var conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            string query = "INSERT INTO SensorData (Value) VALUES (@Value)";
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Value", data.Value);
                await cmd.ExecuteNonQueryAsync();
                _logger.LogInformation("✅ Data saved to database.");
            }
        }
    }
}

public class PayloadData
{
    public int Value { get; set; }
}
*/