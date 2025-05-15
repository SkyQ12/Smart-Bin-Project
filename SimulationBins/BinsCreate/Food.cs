using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationBins.BinsCreate
{
    public class Food
    {
        public string BinUnitId { get; set; }
        public int Level { get; set; }
        public int Status { get; set; }
        public int Full_cnt { get; set; }
        public int Compress_cnt { get; set; }
        public int Vibration { get; set; }
        public int Fault { get; set; }
        public int Flame { get; set; }

        public Food()
        {
        }

        public Food(string binUnitId, int level, int status, int full_cnt, int compress_cnt, int vibration, int fault, int flame)
        {
            BinUnitId = binUnitId;
            Level = level;
            Status = status;
            Full_cnt = full_cnt;
            Compress_cnt = compress_cnt;
            Vibration = vibration;
            Fault = fault;
            Flame = flame;
        }
    }
}
