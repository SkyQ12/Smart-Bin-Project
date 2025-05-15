using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Simulation_Publishers.Mqtt;


/*
namespace SmartBin.Simulation
{
    class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // Cấu hình đọc MqttOptions
                    services.Configure<MqttOptions>(context.Configuration.GetSection("MqttOptions"));

                    // Đăng ký các dịch vụ
                    services.AddSingleton<MqttClient>();

                    // Food
                    services.AddSingleton<Simulation_Publishers.BINS.Food.Bin_10>();
                    services.AddSingleton<Simulation_Publishers.BINS.Food.Bin_11>();
                    services.AddSingleton<Simulation_Publishers.BINS.Food.Bin_12>();
                    services.AddSingleton<Simulation_Publishers.BINS.Food.Bin_13>();
                    services.AddSingleton<Simulation_Publishers.BINS.Food.Bin_14>();
                    services.AddSingleton<Simulation_Publishers.BINS.Food.Bin_15>();
                    services.AddSingleton<Simulation_Publishers.BINS.Food.Bin_16>();
                    services.AddSingleton<Simulation_Publishers.BINS.Food.Bin_17>();
                    services.AddSingleton<Simulation_Publishers.BINS.Food.Bin_18>();
                    services.AddSingleton<Simulation_Publishers.BINS.Food.Bin_19>();
                    services.AddSingleton<Simulation_Publishers.BINS.Food.Bin_20>();

                    // Other
                    services.AddSingleton<Simulation_Publishers.BINS.Other.Bin_10>();
                    services.AddSingleton<Simulation_Publishers.BINS.Other.Bin_11>();
                    services.AddSingleton<Simulation_Publishers.BINS.Other.Bin_12>();
                    services.AddSingleton<Simulation_Publishers.BINS.Other.Bin_13>();
                    services.AddSingleton<Simulation_Publishers.BINS.Other.Bin_14>();
                    services.AddSingleton<Simulation_Publishers.BINS.Other.Bin_15>();
                    services.AddSingleton<Simulation_Publishers.BINS.Other.Bin_16>();
                    services.AddSingleton<Simulation_Publishers.BINS.Other.Bin_17>();
                    services.AddSingleton<Simulation_Publishers.BINS.Other.Bin_18>();
                    services.AddSingleton<Simulation_Publishers.BINS.Other.Bin_19>();
                    services.AddSingleton<Simulation_Publishers.BINS.Other.Bin_20>();

                    // Recycle
                    services.AddSingleton<Simulation_Publishers.BINS.Recycle.Bin_10>();
                    services.AddSingleton<Simulation_Publishers.BINS.Recycle.Bin_11>();
                    services.AddSingleton<Simulation_Publishers.BINS.Recycle.Bin_12>();
                    services.AddSingleton<Simulation_Publishers.BINS.Recycle.Bin_13>();
                    services.AddSingleton<Simulation_Publishers.BINS.Recycle.Bin_14>();
                    services.AddSingleton<Simulation_Publishers.BINS.Recycle.Bin_15>();
                    services.AddSingleton<Simulation_Publishers.BINS.Recycle.Bin_16>();
                    services.AddSingleton<Simulation_Publishers.BINS.Recycle.Bin_17>();
                    services.AddSingleton<Simulation_Publishers.BINS.Recycle.Bin_18>();
                    services.AddSingleton<Simulation_Publishers.BINS.Recycle.Bin_19>();
                    services.AddSingleton<Simulation_Publishers.BINS.Recycle.Bin_20>();
                        
                });

        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Lấy các dịch vụ đã đăng ký trong DI container
            var mqttClient = host.Services.GetRequiredService<MqttClient>();

            // Food
            var bin_10_food = host.Services.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_10>();
            var bin_11_food = host.Services.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_11>();
            var bin_12_food = host.Services.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_12>();
            var bin_13_food = host.Services.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_13>();
            var bin_14_food = host.Services.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_14>();
            var bin_15_food = host.Services.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_15>();
            var bin_16_food = host.Services.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_16>();
            var bin_17_food = host.Services.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_17>();
            var bin_18_food = host.Services.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_18>();
            var bin_19_food = host.Services.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_19>();
            var bin_20_food = host.Services.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_20>();


            // Other
            var bin_10_other = host.Services.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_10>();
            var bin_11_other = host.Services.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_11>();
            var bin_12_other = host.Services.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_12>();
            var bin_13_other = host.Services.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_13>();
            var bin_14_other = host.Services.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_14>();
            var bin_15_other = host.Services.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_15>();
            var bin_16_other = host.Services.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_16>();
            var bin_17_other = host.Services.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_17>();
            var bin_18_other = host.Services.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_18>();
            var bin_19_other = host.Services.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_19>();
            var bin_20_other = host.Services.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_20>();


            // Recycle
            var bin_10_recycle = host.Services.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_10>();
            var bin_11_recycle = host.Services.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_11>();
            var bin_12_recycle = host.Services.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_12>();
            var bin_13_recycle = host.Services.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_13>();
            var bin_14_recycle = host.Services.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_14>();
            var bin_15_recycle = host.Services.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_15>();
            var bin_16_recycle = host.Services.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_16>();
            var bin_17_recycle = host.Services.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_17>();
            var bin_18_recycle = host.Services.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_18>();
            var bin_19_recycle = host.Services.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_19>();
            var bin_20_recycle = host.Services.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_20>();


            // Khởi tạo các bin và sensorPublisher
            var bins = new List<MqttControl> 
            {
                // Food
                bin_10_food,
                bin_11_food,
                bin_12_food,
                bin_13_food,
                bin_14_food,
                bin_15_food,
                bin_16_food,
                bin_17_food,
                bin_18_food,
                bin_19_food,
                bin_20_food,

                // Other
                bin_10_other,
                bin_11_other,
                bin_12_other,
                bin_13_other,
                bin_14_other,
                bin_15_other,
                bin_16_other,
                bin_17_other,
                bin_18_other,
                bin_19_other,
                bin_20_other,

                // Recycle
                bin_10_recycle,
                bin_11_recycle,
                bin_12_recycle,
                bin_13_recycle,
                bin_14_recycle,
                bin_15_recycle,
                bin_16_recycle,
                bin_17_recycle,
                bin_18_recycle,
                bin_19_recycle,
                bin_20_recycle,

            };
            var sensorPublisher = new SensorPublisher(mqttClient, bins);

            // Tạo CancellationTokenSource để quản lý việc hủy bỏ
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            // Bắt đầu mô phỏng và publishing dữ liệu

            // Food
            bin_10_food.StartSimulation();
            bin_11_food.StartSimulation();
            bin_12_food.StartSimulation();
            bin_13_food.StartSimulation();
            bin_14_food.StartSimulation();
            bin_15_food.StartSimulation();
            bin_16_food.StartSimulation();
            bin_17_food.StartSimulation();
            bin_18_food.StartSimulation();
            bin_19_food.StartSimulation();
            bin_20_food.StartSimulation();
            /*
            // Other
            bin_10_other.StartSimulation();
            bin_11_other.StartSimulation();
            bin_12_other.StartSimulation();
            bin_13_other.StartSimulation();
            bin_14_other.StartSimulation();
            bin_15_other.StartSimulation();
            bin_16_other.StartSimulation();
            bin_17_other.StartSimulation();
            bin_18_other.StartSimulation();
            bin_19_other.StartSimulation();
            bin_20_other.StartSimulation();

            // Recycle
            bin_10_recycle.StartSimulation();
            bin_11_recycle.StartSimulation();
            bin_12_recycle.StartSimulation();
            bin_13_recycle.StartSimulation();
            bin_14_recycle.StartSimulation();
            bin_15_recycle.StartSimulation();
            bin_16_recycle.StartSimulation();
            bin_17_recycle.StartSimulation();
            bin_18_recycle.StartSimulation();
            bin_19_recycle.StartSimulation();
            bin_20_recycle.StartSimulation();
            */


/*
            await sensorPublisher.StartPublishingDataAsync(cancellationToken); // Truyền CancellationToken vào đây

            await host.RunAsync(); // Bắt đầu chạy ứng dụng
        }
    }
}
*/