using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBin.Infrastructure.Domain.Resources.CollectedHistories
{
    public class ErrorHistoryViewModel
    {
        public string BinUnitId { get; set; }
        public int Id { get; set; }
        public int ErrorId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
