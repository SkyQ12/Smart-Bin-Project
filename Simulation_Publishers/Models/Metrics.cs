using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Publishers.Models
{
    public class Metrics
    {
        public int Fault { get; set; }
        public int Level { get; set; }
        public int CompressCnt { get; set; }
        public int FullCnt { get; set; }
        public int Status { get; set; }
        public int Flame { get; set; }
        public int Vibration { get; set; }
        public int QR { get; set; }
        public int? Battery { get; set; } 
        public Metrics() { } 

        public Metrics(int fault, int level, int compressCnt, int fullCnt, int status, int flame, int vibration, int qr, int? battery)
        {
            Fault = fault;
            Level = level;
            CompressCnt = compressCnt;
            FullCnt = fullCnt;
            Status = status;
            Flame = flame;
            Vibration = vibration;
            QR = qr;
            Battery = battery;
        }
    }
}
