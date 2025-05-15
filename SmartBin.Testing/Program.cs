using MQTTnet.Client;
using MQTTnet;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


class Program
{
    static async Task Main(string[] args)
    {
        var options = new MqttClientOptionsBuilder()
                            .WithClientId(Guid.NewGuid().ToString())
                            .WithTcpServer("20.41.104.186", 1883)
                            .WithTimeout(TimeSpan.FromSeconds(30))
                            .WithKeepAlivePeriod(TimeSpan.FromSeconds(60))
                            .Build();

        var mqttClient = new MqttFactory().CreateMqttClient();

        // Đăng ký sự kiện nhận tin nhắn
        mqttClient.ApplicationMessageReceivedAsync += async e =>
        {
            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            try
            {
                // Bỏ qua dữ liệu cũ có chứa "name" (ví dụ: Battery, Connected,...)
                if (payload.Contains("\"name\""))
                {
                    return;
                }

                // Deserialize JSON đúng định dạng mong muốn
                var data = JsonConvert.DeserializeObject<PayloadData>(payload);
                if (data != null && data.Id != 0)
                {
                    Console.WriteLine($"[✔] ID: {data.Id}, Value: {data.Value}, Timestamp: {data.Timestamp}");

                    // Lưu vào database
                    await SaveToDatabase(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠ Lỗi khi xử lý dữ liệu: " + ex.Message);
            }
        };

        await mqttClient.ConnectAsync(options);
        Console.WriteLine("[✔] Connected to MQTT broker!");

        string topicToSubscribe = "Smart_bin/Test/Bin_4/Bin_4_Food/Metric/Level";
        await mqttClient.SubscribeAsync(topicToSubscribe);
        Console.WriteLine($"[✔] Subscribed to topic: {topicToSubscribe}");

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Q)
            {
                Console.WriteLine("[✔] Exiting...");
                break;
            }
        }
    }
    
    // ✅ Đưa hàm SaveToDatabase ra ngoài
    static async Task SaveToDatabase(PayloadData data)
    {
        string connectionString = "Server=desktop-hs403t0\\sqlexpress;Database=TestSmartBin;Trusted_Connection=True;TrustServerCertificate=True;";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();

            string query = "INSERT INTO SensorData (Value) VALUES (@Value)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                
                cmd.Parameters.AddWithValue("@Value", data.Value);
                

                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("✅ Data saved to database.");
            }
        }
    }
}

    // ✅ Class dữ liệu
    public class PayloadData
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime Timestamp { get; set; }
    }

