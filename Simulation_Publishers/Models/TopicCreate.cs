using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Publishers.Models
{
    public class TopicCreate
    {
        public TopicCreate() { }
        public static string BuildTopic(string BinUnitId, string Metric)
        {
            string[] parts = BinUnitId.Split('_');
            if (parts.Length != 3)
            {
                throw new ArgumentException("BinUnitId phải có dạng BIN_10_Food");
            }

            string binId = parts[0] + "_" + parts[1];
            return $"Smart_bin/Test/{binId}/{BinUnitId}/Metric/{Metric}";
        }
    }
}
