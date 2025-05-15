using Microsoft.Extensions.Hosting;
using SimulationBins.BinsCreate;
using SimulationBins.Control;
using SimulationBins.MqttClient;

public class SmartBinSimulationService : BackgroundService
{
    private readonly MqttManaged _mqtt;
    private readonly DataPublisher _publisher;

    public SmartBinSimulationService(MqttManaged mqtt, DataPublisher publisher)
    {
        _mqtt = mqtt;
        _publisher = publisher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _mqtt.ConnectAsync();

        var bins = new List<Food>();
        for (int i = 20; i <= 100; i++)
        {
            bins.Add(new Food($"Bin_{i}_Food", 0, 0, 0, 0, 0, 0, 0));
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var bin in bins)
            {
                DataAlgorithm.UpdateData(bin);
                string timestamp = DateTime.Now.ToString("s");

                await _publisher.PublishData(TopicGenerator.GenerateTopic(bin.BinUnitId, "Level"), bin.Level.ToString(), timestamp);
                await _publisher.PublishData(TopicGenerator.GenerateTopic(bin.BinUnitId, "Status"), bin.Status.ToString(), timestamp);
                await _publisher.PublishData(TopicGenerator.GenerateTopic(bin.BinUnitId, "Full_cnt"), bin.Full_cnt.ToString(), timestamp);
                await _publisher.PublishData(TopicGenerator.GenerateTopic(bin.BinUnitId, "Compress_cnt"), bin.Compress_cnt.ToString(), timestamp);
                await _publisher.PublishData(TopicGenerator.GenerateTopic(bin.BinUnitId, "Vibration"), bin.Vibration.ToString(), timestamp);
                await _publisher.PublishData(TopicGenerator.GenerateTopic(bin.BinUnitId, "Fault"), bin.Fault.ToString(), timestamp);
                await _publisher.PublishData(TopicGenerator.GenerateTopic(bin.BinUnitId, "Flame"), bin.Flame.ToString(), timestamp);
            }

            await Task.Delay(1800000, stoppingToken);
        }
    }
}
