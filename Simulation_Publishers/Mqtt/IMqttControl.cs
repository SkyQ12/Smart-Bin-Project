using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Publishers.Mqtt
{
    public interface IMqttControl
    {
        string BinUnitId { get; }
        void StartSimulation();
    }
}
