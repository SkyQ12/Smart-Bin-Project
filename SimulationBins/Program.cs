using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimulationBins.Control;
using SimulationBins.MqttClient;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("MqttConfigure.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<MqttOptions>(context.Configuration.GetSection("MqttOptions"));
        services.AddSingleton<MqttManaged>();
        services.AddSingleton<DataPublisher>();
        services.AddHostedService<SmartBinSimulationService>();
    });

var host = builder.Build();
await host.RunAsync();
