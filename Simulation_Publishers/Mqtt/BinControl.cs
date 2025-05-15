using Microsoft.Extensions.DependencyInjection;
namespace Simulation_Publishers.Mqtt
{
    public class BinControl
    {
        public static void StartAll(IServiceProvider serviceProvider)
        {
            // Đảm bảo rằng các đối tượng được lấy từ DI container
            var bins = new List<IMqttControl>
        {
                // Food
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_10>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_11>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_12>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_13>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_14>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_15>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_16>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_17>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_18>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_19>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Food.Bin_20>(),


                // Other
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_10>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_11>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_12>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_13>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_14>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_15>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_16>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_17>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_18>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_19>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Other.Bin_20>(),

                // Recycle
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_10>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_11>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_12>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_13>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_14>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_15>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_16>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_17>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_18>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_19>(),
            serviceProvider.GetRequiredService<Simulation_Publishers.BINS.Recycle.Bin_20>(),

        };

            foreach (var bin in bins)
            {
                bin.StartSimulation();
            }
        }
    }


}
