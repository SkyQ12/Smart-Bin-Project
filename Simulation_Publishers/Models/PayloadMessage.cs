using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Publishers.Models
{
    public class PayloadMessage
    {
        public string Id { get; set; }
        public object Value { get; set; }
        public DateTime Timestamp { get; set; }

        public PayloadMessage()
        {
        }

        public PayloadMessage(string id, object value, DateTime timestamp)
        {
            Id = id;
            Value = value;
            Timestamp = timestamp;
        }
    }
}
