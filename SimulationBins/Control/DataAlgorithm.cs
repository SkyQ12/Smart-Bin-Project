using SimulationBins.BinsCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationBins.BinsCreate;

namespace SimulationBins.Control
{
    public class DataAlgorithm
    {
        private static readonly Random _random = new();

        public static void UpdateData(Food bin)
        {
            bin.Level = _random.Next(0, 101);         
            bin.Status = _random.Next(0, 2);            
            bin.Full_cnt = _random.Next(0, 100);
            bin.Compress_cnt = _random.Next(0, 100);
            bin.Vibration = _random.Next(0, 2);         
            bin.Fault = _random.Next(0, 2);
            bin.Flame = _random.Next(0, 2);
        }
    }
}
