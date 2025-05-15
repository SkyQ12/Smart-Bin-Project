using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Publishers.Models.BINS
{
    public class FoodCreate
    {
        public string BinUnitId { get; set; }

        // Flags
        public int ConpressFlag { get; set; }
        public int CollectFlag { get; set; }
        public int FullFlag { get; set; }

        // Metrics
        public string QR { get; set; } // ID = 1
        public int Fault { get; set; } // ID = 2
        public int Level { get; set; } // ID = 3
        public int Compress_cnt { get; set; } // ID = 4
        public int Full_cnt { get; set; } // ID = 5
        public int Status { get; set; } // ID = 6
        public int Flame { get; set; } // ID = 7
        public int Vibration { get; set; } // ID = 8

        public FoodCreate()
        {
        }

        public FoodCreate(string binUnitId, int conpressFlag, int collectFlag, int fullFlag, string qR, int fault, int level, int compress_cnt, int full_cnt, int status, int flame, int vibration)
        {
            BinUnitId = binUnitId;
            ConpressFlag = conpressFlag;
            CollectFlag = collectFlag;
            FullFlag = fullFlag;
            QR = qR;
            Fault = fault;
            Level = level;
            Compress_cnt = compress_cnt;
            Full_cnt = full_cnt;
            Status = status;
            Flame = flame;
            Vibration = vibration;
        }
    }
}
