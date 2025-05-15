using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBin.Infrastructure.MqttClients
{
    public enum MetricType
    {
        Level,
        Fault,
        CompressCnt,
        FullCnt,
        Status,
        Flame,
        Vibration,
        Battery
    }
}
