using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBin.Infrastructure.Domain.Resources.Points
{
    public class PointChangeHistoryViewModel
    {
        public int OldPoint {  get; set; }
        public int NewPoint { get; set; }
        public DateTime PointChangedTime { get; set; }
    }
}
