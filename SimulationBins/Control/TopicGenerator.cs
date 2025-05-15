using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationBins.Control
{
    public class TopicGenerator
    {
        public static string GenerateTopic(string binUnitId, string metric)
        {
            string BinId = binUnitId.Split('_')[1];
            string Topic = "Smart_bin/Test/Bin_" + BinId + "/" + binUnitId + "/Metric/" + metric;
            Console.WriteLine(Topic);
            return Topic;
        }
    }
}
